﻿using System;
using System.Diagnostics;
using System.Globalization;
using System.IO.Ports;
using System.Threading;
using Ftdi;

namespace EdiabasLib
{
    static class EdFtdiInterface
    {
        public const string PortID = "FTDI";
        private const int writeTimeout = 500;      // write timeout [ms]
        private static readonly CultureInfo culture = CultureInfo.CreateSpecificCulture("en");
        private static readonly long tickResolMs = Stopwatch.Frequency / 1000;
        private static IntPtr handleFtdi = (IntPtr)0;
        private static int currentBaudRate = 0;
        private static int currentWordLength = 0;
        private static Parity currentParity = Parity.None;

        public static bool InterfaceConnect(string port)
        {
            if (handleFtdi != (IntPtr)0)
            {
                return true;
            }
            try
            {
                Ftd2xx.FT_STATUS ftStatus;
                if (!port.ToUpper(culture).StartsWith(PortID))
                {
                    return false;
                }
                uint usbIndex = Convert.ToUInt32(port.Remove(0, 4));

                ftStatus = Ftd2xx.FT_Open(usbIndex, out handleFtdi);
                if (ftStatus != Ftd2xx.FT_STATUS.FT_OK)
                {
                    InterfaceDisconnect();
                    return false;
                }

                ftStatus = Ftd2xx.FT_SetLatencyTimer(handleFtdi, 2);
                if (ftStatus != Ftd2xx.FT_STATUS.FT_OK)
                {
                    InterfaceDisconnect();
                    return false;
                }

                ftStatus = Ftd2xx.FT_SetBaudRate(handleFtdi, Ftd2xx.FT_BAUD_9600);
                if (ftStatus != Ftd2xx.FT_STATUS.FT_OK)
                {
                    InterfaceDisconnect();
                    return false;
                }
                currentBaudRate = 9600;

                ftStatus = Ftd2xx.FT_SetDataCharacteristics(handleFtdi, Ftd2xx.FT_BITS_8, Ftd2xx.FT_STOP_BITS_1, Ftd2xx.FT_PARITY_NONE);
                if (ftStatus != Ftd2xx.FT_STATUS.FT_OK)
                {
                    InterfaceDisconnect();
                    return false;
                }
                currentWordLength = 8;
                currentParity = Parity.None;

                ftStatus = Ftd2xx.FT_SetTimeouts(handleFtdi, 0, writeTimeout);
                if (ftStatus != Ftd2xx.FT_STATUS.FT_OK)
                {
                    InterfaceDisconnect();
                    return false;
                }

                ftStatus = Ftd2xx.FT_SetFlowControl(handleFtdi, Ftd2xx.FT_FLOW_NONE, 0, 0);
                if (ftStatus != Ftd2xx.FT_STATUS.FT_OK)
                {
                    InterfaceDisconnect();
                    return false;
                }

                ftStatus = Ftd2xx.FT_ClrDtr(handleFtdi);
                if (ftStatus != Ftd2xx.FT_STATUS.FT_OK)
                {
                    InterfaceDisconnect();
                    return false;
                }

                ftStatus = Ftd2xx.FT_ClrRts(handleFtdi);
                if (ftStatus != Ftd2xx.FT_STATUS.FT_OK)
                {
                    InterfaceDisconnect();
                    return false;
                }

                ftStatus = Ftd2xx.FT_Purge(handleFtdi, Ftd2xx.FT_PURGE_TX | Ftd2xx.FT_PURGE_RX);
                if (ftStatus != Ftd2xx.FT_STATUS.FT_OK)
                {
                    InterfaceDisconnect();
                    return false;
                }
            }
            catch (Exception)
            {
                InterfaceDisconnect();
                return false;
            }
            return true;
        }

        public static bool InterfaceDisconnect()
        {
            if (handleFtdi != (IntPtr)0)
            {
                Ftd2xx.FT_Close(handleFtdi);
                handleFtdi = (IntPtr)0;
            }
            return true;
        }

        public static bool InterfaceSetConfig(int baudRate, int dataBits, Parity parity)
        {
            if (handleFtdi == (IntPtr)0)
            {
                return false;
            }
            try
            {
                Ftd2xx.FT_STATUS ftStatus;

                ftStatus = Ftd2xx.FT_SetBaudRate(handleFtdi, (uint)baudRate);
                if (ftStatus != Ftd2xx.FT_STATUS.FT_OK)
                {
                    return false;
                }

                byte wordLength;

                switch (dataBits)
                {
                    case 5:
                        wordLength = Ftd2xx.FT_BITS_5;
                        break;

                    case 6:
                        wordLength = Ftd2xx.FT_BITS_6;
                        break;

                    case 7:
                        wordLength = Ftd2xx.FT_BITS_7;
                        break;

                    case 8:
                        wordLength = Ftd2xx.FT_BITS_8;
                        break;

                    default:
                        return false;
                }

                byte parityLocal;

                switch (parity)
                {
                    case Parity.None:
                        parityLocal = Ftd2xx.FT_PARITY_NONE;
                        break;

                    case Parity.Even:
                        parityLocal = Ftd2xx.FT_PARITY_EVEN;
                        break;

                    case Parity.Odd:
                        parityLocal = Ftd2xx.FT_PARITY_ODD;
                        break;

                    case Parity.Mark:
                        parityLocal = Ftd2xx.FT_PARITY_MARK;
                        break;

                    case Parity.Space:
                        parityLocal = Ftd2xx.FT_PARITY_SPACE;
                        break;

                    default:
                        return false;
                }

                ftStatus = Ftd2xx.FT_SetDataCharacteristics(handleFtdi, wordLength, Ftd2xx.FT_STOP_BITS_1, parityLocal);
                if (ftStatus != Ftd2xx.FT_STATUS.FT_OK)
                {
                    return false;
                }
                currentBaudRate = baudRate;
                currentWordLength = wordLength;
                currentParity = parity;
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static bool InterfaceSetDtr(bool dtr)
        {
            if (handleFtdi == (IntPtr)0)
            {
                return false;
            }
            try
            {
                Ftd2xx.FT_STATUS ftStatus;

                if (dtr)
                {
                    ftStatus = Ftd2xx.FT_SetDtr(handleFtdi);
                }
                else
                {
                    ftStatus = Ftd2xx.FT_ClrDtr(handleFtdi);
                }
                if (ftStatus != Ftd2xx.FT_STATUS.FT_OK)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static bool InterfaceSetRts(bool rts)
        {
            if (handleFtdi == (IntPtr)0)
            {
                return false;
            }
            try
            {
                Ftd2xx.FT_STATUS ftStatus;

                if (rts)
                {
                    ftStatus = Ftd2xx.FT_SetRts(handleFtdi);
                }
                else
                {
                    ftStatus = Ftd2xx.FT_ClrRts(handleFtdi);
                }
                if (ftStatus != Ftd2xx.FT_STATUS.FT_OK)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static bool InterfaceGetDsr(out bool dsr)
        {
            dsr = false;
            if (handleFtdi == (IntPtr)0)
            {
                return false;
            }
            try
            {
                dsr = false;
                uint modemStatus = 0x0000;
                Ftd2xx.FT_STATUS ftStatus = Ftd2xx.FT_GetModemStatus(handleFtdi, ref modemStatus);
                if (ftStatus != Ftd2xx.FT_STATUS.FT_OK)
                {
                    return false;
                }
                dsr = (modemStatus & 0x20) != 0;
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static bool InterfaceSetBreak(bool enable)
        {
            if (handleFtdi == (IntPtr)0)
            {
                return false;
            }
            try
            {
                Ftd2xx.FT_STATUS ftStatus;

                if (enable)
                {
                    ftStatus = Ftd2xx.FT_SetBreakOn(handleFtdi);
                }
                else
                {
                    ftStatus = Ftd2xx.FT_SetBreakOff(handleFtdi);
                }
                if (ftStatus != Ftd2xx.FT_STATUS.FT_OK)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static bool InterfacePurgeInBuffer()
        {
            if (handleFtdi == (IntPtr)0)
            {
                return false;
            }
            try
            {   // ftdi
                Ftd2xx.FT_STATUS ftStatus = Ftd2xx.FT_Purge(handleFtdi, Ftd2xx.FT_PURGE_RX);
                if (ftStatus != Ftd2xx.FT_STATUS.FT_OK)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static bool InterfaceSendData(byte[] sendData, int length, bool setDtr)
        {
            if (handleFtdi == (IntPtr)0)
            {
                return false;
            }
            try
            {   // ftdi
                Ftd2xx.FT_STATUS ftStatus;
                uint bytesWritten = 0;

                int bitCount = (currentParity == Parity.None) ? (currentWordLength + 2) : (currentWordLength + 3);
                double byteTime = 1.0d / currentBaudRate * 1000 * bitCount;
                if (setDtr)
                {
                    long waitTime = (long)((0.3d + byteTime * length) * tickResolMs);
                    ftStatus = Ftd2xx.FT_SetDtr(handleFtdi);
                    if (ftStatus != Ftd2xx.FT_STATUS.FT_OK)
                    {
                        return false;
                    }
                    long startTime = Stopwatch.GetTimestamp();
#if WindowsCE
                    const int sendBlockSize = 4;
                    for (int i = 0; i < length; i += sendBlockSize)
                    {
                        int sendLength = length - i;
                        if (sendLength > sendBlockSize) sendLength = sendBlockSize;
                        ftStatus = Ftd2xx.FT_WriteWrapper(_handleFtdi, sendData, sendLength, i, out bytesWritten);
                        if (ftStatus != Ftd2xx.FT_STATUS.FT_OK)
                        {
                            return false;
                        }
                    }
#else
                    ftStatus = Ftd2xx.FT_WriteWrapper(handleFtdi, sendData, length, 0, out bytesWritten);
                    if (ftStatus != Ftd2xx.FT_STATUS.FT_OK)
                    {
                        return false;
                    }
#endif
                    while ((Stopwatch.GetTimestamp() - startTime) < waitTime)
                    {
                    }
                    ftStatus = Ftd2xx.FT_ClrDtr(handleFtdi);
                    if (ftStatus != Ftd2xx.FT_STATUS.FT_OK)
                    {
                        return false;
                    }
                }
                else
                {
                    long waitTime = (long)(byteTime * length);
#if WindowsCE
                    const int sendBlockSize = 4;
                    for (int i = 0; i < length; i += sendBlockSize)
                    {
                        int sendLength = length - i;
                        if (sendLength > sendBlockSize) sendLength = sendBlockSize;
                        ftStatus = Ftd2xx.FT_WriteWrapper(_handleFtdi, sendData, sendLength, i, out bytesWritten);
                        if (ftStatus != Ftd2xx.FT_STATUS.FT_OK)
                        {
                            return false;
                        }
                    }
#else
                    ftStatus = Ftd2xx.FT_WriteWrapper(handleFtdi, sendData, length, 0, out bytesWritten);
                    if (ftStatus != Ftd2xx.FT_STATUS.FT_OK)
                    {
                        return false;
                    }
#endif
                    if (waitTime > 10)
                    {
                        Thread.Sleep((int)waitTime);
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static bool InterfaceReceiveData(byte[] receiveData, int offset, int length, int timeout, int timeoutTelEnd, EdiabasNet ediabasLog)
        {
            if (handleFtdi == (IntPtr)0)
            {
                return false;
            }
#if WindowsCE
            if (timeout < 500)
            {
                timeout = _readTimeoutMin;
            }
            if (timeoutTelEnd < 500)
            {
                timeoutTelEnd = _readTimeoutMin;
            }
#else
            // add extra delay for internal signal transitions
            timeout += 20;
            timeoutTelEnd += 20;
#endif
            try
            {   // ftdi
                Ftd2xx.FT_STATUS ftStatus;
                uint bytesRead = 0;
                int recLen = 0;

                ftStatus = Ftd2xx.FT_SetTimeouts(handleFtdi, (uint)timeout, writeTimeout);
                if (ftStatus != Ftd2xx.FT_STATUS.FT_OK)
                {
                    return false;
                }
                ftStatus = Ftd2xx.FT_ReadWrapper(handleFtdi, receiveData, 1, offset, out bytesRead);
                if (ftStatus != Ftd2xx.FT_STATUS.FT_OK)
                {
                    return false;
                }
                recLen = (int)bytesRead;
                if (recLen < 1)
                {
                    return false;
                }
                if (recLen < length)
                {
                    ftStatus = Ftd2xx.FT_SetTimeouts(handleFtdi, (uint)timeoutTelEnd, writeTimeout);
                    if (ftStatus != Ftd2xx.FT_STATUS.FT_OK)
                    {
                        return false;
                    }
                    ftStatus = Ftd2xx.FT_ReadWrapper(handleFtdi, receiveData, length - recLen, offset + recLen, out bytesRead);
                    if (ftStatus != Ftd2xx.FT_STATUS.FT_OK)
                    {
                        return false;
                    }
                    recLen += (int)bytesRead;
                }
                if (ediabasLog != null)
                {
                    ediabasLog.LogData(EdiabasNet.ED_LOG_LEVEL.IFH, receiveData, offset, recLen, "Rec ");
                }
                if (recLen < length)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

    }
}
