﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace ExtractEcuFunctions
{
    [XmlInclude(typeof(RefEcuVariant))]
    [XmlInclude(typeof(EcuFuncStruct))]
    public class EcuVariant
    {
        public EcuVariant()
        {
        }

        public EcuVariant(string id, string titleEn, string titleDe, string titleRu, string groupId, List<string> groupFunctionIds)
        {
            Id = id;
            TitleEn = titleEn;
            TitleDe = titleDe;
            TitleRu = titleRu;
            GroupId = groupId;
            GroupFunctionIds = groupFunctionIds;
        }

        public string ToString(string prefix)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(prefix + "VARIANT:");
            sb.Append(this.PropertyList(prefix + " "));
            if (GroupFunctionIds != null)
            {
                foreach (string GroupFunctionId in GroupFunctionIds)
                {
                    sb.Append(prefix + " " + GroupFunctionId);
                }
            }

            if (RefEcuVariantList != null)
            {
                foreach (RefEcuVariant refEcuVariant in RefEcuVariantList)
                {
                    sb.Append(refEcuVariant.ToString(prefix + " "));
                }
            }

            if (EcuFuncStructList != null)
            {
                foreach (EcuFuncStruct ecuFuncStruct in EcuFuncStructList)
                {
                    sb.Append(ecuFuncStruct.ToString(prefix + " "));
                }
            }

            return sb.ToString();
        }

        public override string ToString()
        {
            return ToString("");
        }

        [XmlElement]
        public string Id { get; }
        [XmlElement]
        public string TitleEn { get; }
        [XmlElement]
        public string TitleDe { get; }
        [XmlElement]
        public string TitleRu { get; }
        [XmlElement]
        public string GroupId { get; }
        [XmlArray]
        public List<string> GroupFunctionIds { get; }
        [XmlArray]
        public List<RefEcuVariant> RefEcuVariantList { get; set; }
        [XmlArray]
        public List<EcuFuncStruct> EcuFuncStructList { get; set; }
    }

    [XmlInclude(typeof(EcuFixedFuncStruct))]
    public class RefEcuVariant
    {
        public RefEcuVariant()
        {
        }

        public RefEcuVariant(string id, string ecuVariantId)
        {
            Id = id;
            EcuVariantId = ecuVariantId;
        }

        public string ToString(string prefix)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(prefix + "REFVARIANT:");
            sb.Append(this.PropertyList(prefix + " "));

            if (FixedFuncStructList != null)
            {
                foreach (EcuFixedFuncStruct ecuFixedFuncStruct in FixedFuncStructList)
                {
                    sb.Append(ecuFixedFuncStruct.ToString(prefix + " "));
                }
            }
            return sb.ToString();
        }

        public override string ToString()
        {
            return ToString("");
        }

        [XmlElement]
        public string Id { get; }
        [XmlElement]
        public string EcuVariantId { get; }
        [XmlArray]
        public List<EcuFixedFuncStruct> FixedFuncStructList { get; set; }
    }

    public class EcuVarFunc
    {
        public EcuVarFunc()
        {
        }

        public EcuVarFunc(string id, string groupFuncId)
        {
            Id = id;
            GroupFuncId = groupFuncId;
        }

        public string ToString(string prefix)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(prefix + "VARFUNC:");
            sb.Append(this.PropertyList(prefix + " "));
            return sb.ToString();
        }

        public override string ToString()
        {
            return ToString("");
        }

        [XmlElement]
        public string Id { get; }
        [XmlElement]
        public string GroupFuncId { get; }
    }

    [XmlInclude(typeof(EcuFixedFuncStruct))]
    public class EcuFuncStruct
    {
        public EcuFuncStruct()
        {
        }

        public EcuFuncStruct(string id, string titleEn, string titleDe, string titleRu)
        {
            Id = id;
            TitleEn = titleEn;
            TitleDe = titleDe;
            TitleRu = titleRu;
        }

        public string ToString(string prefix)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(prefix + "FUNC:");
            sb.Append(this.PropertyList(prefix + " "));
            if (FixedFuncStructList != null)
            {
                foreach (EcuFixedFuncStruct ecuFixedFuncStruct in FixedFuncStructList)
                {
                    sb.Append(ecuFixedFuncStruct.ToString(prefix + " "));
                }
            }

            return sb.ToString();
        }

        public override string ToString()
        {
            return ToString("");
        }

        [XmlElement]
        public string Id { get; }
        [XmlElement]
        public string TitleEn { get; }
        [XmlElement]
        public string TitleDe { get; }
        [XmlElement]
        public string TitleRu { get; }
        [XmlArray]
        public List<EcuFixedFuncStruct> FixedFuncStructList { get; set; }
    }

    [XmlInclude(typeof(EcuJobParameter))]
    [XmlInclude(typeof(EcuJobResult))]
    public class EcuJob
    {
        public EcuJob()
        {
        }

        public EcuJob(string id, string funcNameJob, string name)
        {
            Id = id;
            FuncNameJob = funcNameJob;
            Name = name;
        }

        public string ToString(string prefix)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(prefix + "JOB:");
            sb.Append(this.PropertyList(prefix + " "));
            if (EcuJobParList != null)
            {
                foreach (EcuJobParameter ecuJobParameter in EcuJobParList)
                {
                    sb.Append(ecuJobParameter.ToString(prefix + " "));
                }
            }

            if (EcuJobResultList != null)
            {
                foreach (EcuJobResult ecuJobResult in EcuJobResultList)
                {
                    sb.Append(ecuJobResult.ToString(prefix + " "));
                }
            }

            return sb.ToString();
        }

        public override string ToString()
        {
            return ToString("");
        }

        [XmlElement]
        public string Id { get; }
        [XmlElement]
        public string FuncNameJob { get; }
        [XmlElement]
        public string Name { get; }
        [XmlArray]
        public List<EcuJobParameter> EcuJobParList { get; set; }
        [XmlArray]
        public List<EcuJobResult> EcuJobResultList { get; set; }
    }

    public class EcuJobParameter
    {
        public EcuJobParameter()
        {
        }

        public EcuJobParameter(string id, string value, string adapterPath, string name)
        {
            Id = id;
            Value = value;
            AdapterPath = adapterPath;
            Name = name;
        }

        public string ToString(string prefix)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(prefix + "JOB:");
            sb.Append(this.PropertyList(prefix + " "));
            return sb.ToString();
        }

        public override string ToString()
        {
            return ToString("");
        }

        [XmlElement]
        public string Id { get; }
        [XmlElement]
        public string Value { get; }
        [XmlElement]
        public string AdapterPath { get; }
        [XmlElement]
        public string Name { get; }
    }

    [XmlInclude(typeof(EcuResultStateValue))]
    public class EcuJobResult
    {
        public EcuJobResult()
        {
        }

        public EcuJobResult(string id, string titleEn, string titleDe, string titleRu, string adapterPath,
            string name, string location, string unit, string unitFixed, string format, string mult, string offset, string round, string numberFormat)
        {
            Id = id;
            TitleEn = titleEn;
            TitleDe = titleDe;
            TitleRu = titleRu;
            AdapterPath = adapterPath;
            Name = name;
            Location = location;
            Unit = unit;
            UnitFixed = unitFixed;
            Format = format;
            Mult = mult;
            Offset = offset;
            Round = round;
            NumberFormat = numberFormat;
        }

        public string ToString(string prefix)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(prefix + "RESULT:");
            sb.Append(this.PropertyList(prefix + " "));
            if (EcuResultStateValueList != null)
            {
                foreach (EcuResultStateValue ecuResultStateValue in EcuResultStateValueList)
                {
                    sb.Append(ecuResultStateValue.ToString(prefix + " "));
                }
            }

            return sb.ToString();
        }

        public override string ToString()
        {
            return ToString("");
        }

        [XmlElement]
        public string Id { get; }
        [XmlElement]
        public string TitleEn { get; }
        [XmlElement]
        public string TitleDe { get; }
        [XmlElement]
        public string TitleRu { get; }
        [XmlElement]
        public string AdapterPath { get; }
        [XmlElement]
        public string Name { get; }
        [XmlElement]
        public string Location { get; }
        [XmlElement]
        public string Unit { get; }
        [XmlElement]
        public string UnitFixed { get; }
        [XmlElement]
        public string Format { get; }
        [XmlElement]
        public string Mult { get; }
        [XmlElement]
        public string Offset { get; }
        [XmlElement]
        public string Round { get; }
        [XmlElement]
        public string NumberFormat { get; }
        [XmlArray]
        public List<EcuResultStateValue> EcuResultStateValueList { get; set; }
    }

    public class EcuResultStateValue
    {
        public EcuResultStateValue()
        {
        }

        public EcuResultStateValue(string id, string titleEn, string titleDe, string titleRu,
            string stateValue, string validFrom, string validTo, string parentId)
        {
            Id = id;
            TitleEn = titleEn;
            TitleDe = titleDe;
            TitleRu = titleRu;
            StateValue = stateValue;
            ValidFrom = validFrom;
            ValidTo = validTo;
            ParentId = parentId;
        }

        public string ToString(string prefix)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(prefix + "STATEVALUE:");
            sb.Append(this.PropertyList(prefix + " "));
            return sb.ToString();
        }

        public override string ToString()
        {
            return ToString("");
        }

        [XmlElement]
        public string Id { get; }
        [XmlElement]
        public string TitleEn { get; }
        [XmlElement]
        public string TitleDe { get; }
        [XmlElement]
        public string TitleRu { get; }
        [XmlElement]
        public string StateValue { get; }
        [XmlElement]
        public string ValidFrom { get; }
        [XmlElement]
        public string ValidTo { get; }
        [XmlElement]
        public string ParentId { get; }
    }

    public class EcuFixedFuncStruct
    {
        public EcuFixedFuncStruct()
        {
        }

        public EcuFixedFuncStruct(string id, string nodeClass, string nodeClassName, string titleEn, string titleDe, string titleRu,
            string prepOpEn, string prepOpDe, string prepOpRu,
            string procOpEn, string procOpDe, string procOpRu,
            string postOpEn, string postOpDe, string postOpRu)
        {
            Id = id;
            NodeClass = nodeClass;
            NodeClassName = nodeClassName;
            TitleEn = titleEn;
            TitleDe = titleDe;
            TitleRu = titleRu;
            PrepOpEn = prepOpEn;
            PrepOpDe = prepOpDe;
            PrepOpRu = prepOpRu;
            ProcOpEn = procOpEn;
            ProcOpDe = procOpDe;
            ProcOpRu = procOpRu;
            PostOpEn = postOpEn;
            PostOpDe = postOpDe;
            PostOpRu = postOpRu;
        }

        public string ToString(string prefix)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(prefix + "FIXEDFUNC:");
            sb.Append(this.PropertyList(prefix + " "));
            if (EcuJobList != null)
            {
                foreach (EcuJob ecuJob in EcuJobList)
                {
                    sb.Append(ecuJob.ToString(prefix + " "));
                }
            }

            return sb.ToString();
        }

        public override string ToString()
        {
            return ToString("");
        }

        [XmlElement]
        public string Id { get; }
        [XmlElement]
        public string NodeClass { get; }
        [XmlElement]
        public string NodeClassName { get; }
        [XmlElement]
        public string TitleEn { get; }
        [XmlElement]
        public string TitleDe { get; }
        [XmlElement]
        public string TitleRu { get; }
        [XmlElement]
        public string PrepOpEn { get; }
        [XmlElement]
        public string PrepOpDe { get; }
        [XmlElement]
        public string PrepOpRu { get; }
        [XmlElement]
        public string ProcOpEn { get; }
        [XmlElement]
        public string ProcOpDe { get; }
        [XmlElement]
        public string ProcOpRu { get; }
        [XmlElement]
        public string PostOpEn { get; }
        [XmlElement]
        public string PostOpDe { get; }
        [XmlElement]
        public string PostOpRu { get; }
        [XmlElement]
        public List<EcuJob> EcuJobList { get; set; }
    }

    static class Program
    {
        static int Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            TextWriter outTextWriter = args.Length > 0 ? Console.Out : null;

            if (args.Length < 1)
            {
                outTextWriter?.WriteLine("No Database specified");
                return 1;
            }
            if (args.Length < 2)
            {
                outTextWriter?.WriteLine("No output directory specified");
                return 1;
            }

            string outDir = args[1];
            if (string.IsNullOrEmpty(outDir))
            {
                outTextWriter?.WriteLine("Output directory empty");
                return 1;
            }

            string ecuName = null;
            if (args.Length >= 3)
            {
                ecuName = args[2];
            }

            try
            {
                string outDirSub = Path.Combine(outDir, "EcuFunctions");
                try
                {
                    if (Directory.Exists(outDirSub))
                    {
                        Directory.Delete(outDirSub);
                    }
                    Directory.CreateDirectory(outDirSub);
                }
                catch (Exception)
                {
                    // ignored
                }

                string connection = "Data Source=\"" + args[0] + "\";";
                using (SQLiteConnection mDbConnection = new SQLiteConnection(connection))
                {
                    mDbConnection.SetPassword("6505EFBDC3E5F324");
                    mDbConnection.Open();

                    List<String> ecuNameList;
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (string.IsNullOrEmpty(ecuName))
                    {
                        ecuNameList = GetEcuNameList(mDbConnection);
                    }
                    else
                    {
                        ecuNameList = new List<string> { ecuName };
                    }

                    foreach (string name in ecuNameList)
                    {
                        outTextWriter?.WriteLine("*** ECU: {0} ***", name);
                        EcuVariant ecuVariant = GetEcuVariantFunctions(outTextWriter, mDbConnection, name);

                        if (ecuVariant != null)
                        {
                            outTextWriter?.WriteLine(ecuVariant);

                            string xmlFile = Path.Combine(outDirSub, name.ToLowerInvariant() + ".xml");

                            XmlSerializer serializer = new XmlSerializer(ecuVariant.GetType());
                            using (TextWriter writer = new StreamWriter(xmlFile))
                            {
                                serializer.Serialize(writer, ecuVariant);
                            }
                        }
                    }

                    mDbConnection.Close();
                }
            }
            catch (Exception e)
            {
                outTextWriter?.WriteLine(e);
            }
            return 0;
        }

        public static string PropertyList(this object obj, string prefix)
        {
            StringBuilder sb = new StringBuilder();
            PropertyInfo[] props = obj.GetType().GetProperties();
            foreach (PropertyInfo p in props)
            {
                if (p.PropertyType == typeof(string))
                {
                    string value = p.GetValue(obj, null).ToString();
                    if (!string.IsNullOrEmpty(value))
                    {
                        sb.AppendLine(prefix + p.Name + ": " + value);
                    }
                }
            }
            return sb.ToString();
        }

        public static string PropertyList(this object obj)
        {
            return obj.PropertyList("");
        }

        private static List<string> GetEcuNameList(SQLiteConnection mDbConnection)
        {
            List<string> ecuNameList = new List<string>();
            string sql = @"SELECT NAME FROM XEP_ECUVARIANTS";
            SQLiteCommand command = new SQLiteCommand(sql, mDbConnection);
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    ecuNameList.Add(reader["NAME"].ToString());
                }
            }

            return ecuNameList;
        }

        private static EcuVariant GetEcuVariant(SQLiteConnection mDbConnection, string ecuName)
        {
            EcuVariant ecuVariant = null;
            string sql = string.Format(@"SELECT ID, TITLE_ENUS, TITLE_DEDE, TITLE_RU, ECUGROUPID FROM XEP_ECUVARIANTS WHERE (lower(NAME) = '{0}')", ecuName.ToLowerInvariant());
            SQLiteCommand command = new SQLiteCommand(sql, mDbConnection);
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string groupId = reader["ECUGROUPID"].ToString();
                    ecuVariant = new EcuVariant(reader["ID"].ToString(),
                        reader["TITLE_ENUS"].ToString(),
                        reader["TITLE_DEDE"].ToString(),
                        reader["TITLE_RU"].ToString(),
                        groupId,
                        GetEcuGroupFunctionIds(mDbConnection, groupId));
                }
            }

            return ecuVariant;
        }

        private static List<string> GetEcuGroupFunctionIds(SQLiteConnection mDbConnection, string groupId)
        {
            List<string> ecuGroupFunctionIds = new List<string>();
            string sql = string.Format(@"SELECT ID FROM XEP_ECUGROUPFUNCTIONS WHERE ECUGROUPID = {0}", groupId);
            SQLiteCommand command = new SQLiteCommand(sql, mDbConnection);
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    ecuGroupFunctionIds.Add(reader["ID"].ToString());
                }
            }

            return ecuGroupFunctionIds;
        }

        private static string GetNodeClassName(SQLiteConnection mDbConnection, string nodeClass)
        {
            string sql = string.Format(@"SELECT NAME FROM XEP_NODECLASSES WHERE ID = {0}", nodeClass);
            SQLiteCommand command = new SQLiteCommand(sql, mDbConnection);
            string result = string.Empty;
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    result = reader["NAME"].ToString();
                }
            }

            return result;
        }

        private static List<EcuJob> GetFixedFuncStructJobsList(SQLiteConnection mDbConnection, EcuFixedFuncStruct ecuFixedFuncStruct)
        {
            List<EcuJob> ecuJobList = new List<EcuJob>();
            string sql = string.Format(@"SELECT JOBS.ID JOBID, FUNCTIONNAMEJOB, NAME " +
                                       "FROM XEP_ECUJOBS JOBS, XEP_REFECUJOBS REFJOBS WHERE JOBS.ID = REFJOBS.ECUJOBID AND REFJOBS.ID = {0}", ecuFixedFuncStruct.Id);
            SQLiteCommand command = new SQLiteCommand(sql, mDbConnection);
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    ecuJobList.Add(new EcuJob(reader["JOBID"].ToString(),
                        reader["FUNCTIONNAMEJOB"].ToString(),
                        reader["NAME"].ToString()));
                }
            }

            foreach (EcuJob ecuJob in ecuJobList)
            {
                List<EcuJobParameter> ecuJobParList = new List<EcuJobParameter>();
                sql = string.Format(
                    @"SELECT PARAM.ID PARAMID, PARAMVALUE, FUNCTIONNAMEPARAMETER, ADAPTERPATH, NAME, ECUJOBID " +
                    "FROM XEP_ECUPARAMETERS PARAM, XEP_REFECUPARAMETERS REFPARAM WHERE " +
                    "PARAM.ID = REFPARAM.ECUPARAMETERID AND REFPARAM.ID = {0} AND PARAM.ECUJOBID = {1}", ecuFixedFuncStruct.Id, ecuJob.Id);
                command = new SQLiteCommand(sql, mDbConnection);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ecuJobParList.Add(new EcuJobParameter(reader["PARAMID"].ToString(),
                            reader["PARAMVALUE"].ToString(),
                            reader["ADAPTERPATH"].ToString(),
                            reader["NAME"].ToString()));
                    }
                }

                ecuJob.EcuJobParList = ecuJobParList;

                List<EcuJobResult> ecuJobResultList = new List<EcuJobResult>();
                sql = string.Format(
                    @"SELECT RESULTS.ID RESULTID, TITLE_ENUS, TITLE_DEDE, TITLE_RU, ADAPTERPATH, NAME, LOCATION, UNIT, UNITFIXED, FORMAT, MULTIPLIKATOR, OFFSET, RUNDEN, ZAHLENFORMAT, ECUJOBID " +
                    "FROM XEP_ECURESULTS RESULTS, XEP_REFECURESULTS REFRESULTS WHERE " +
                    "ECURESULTID = RESULTS.ID AND REFRESULTS.ID = {0} AND RESULTS.ECUJOBID = {1}", ecuFixedFuncStruct.Id, ecuJob.Id);
                command = new SQLiteCommand(sql, mDbConnection);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ecuJobResultList.Add(new EcuJobResult(reader["RESULTID"].ToString(),
                            reader["TITLE_ENUS"].ToString(),
                            reader["TITLE_DEDE"].ToString(),
                            reader["TITLE_RU"].ToString(),
                            reader["ADAPTERPATH"].ToString(),
                            reader["NAME"].ToString(),
                            reader["LOCATION"].ToString(),
                            reader["UNIT"].ToString(),
                            reader["UNITFIXED"].ToString(),
                            reader["FORMAT"].ToString(),
                            reader["MULTIPLIKATOR"].ToString(),
                            reader["OFFSET"].ToString(),
                            reader["RUNDEN"].ToString(),
                            reader["ZAHLENFORMAT"].ToString()));
                    }
                }

                foreach (EcuJobResult ecuJobResult in ecuJobResultList)
                {
                    ecuJobResult.EcuResultStateValueList = GetResultStateValueList(mDbConnection, ecuJobResult);
                }

                ecuJob.EcuJobResultList = ecuJobResultList;
            }

            return ecuJobList;
        }

        private static List<EcuResultStateValue> GetResultStateValueList(SQLiteConnection mDbConnection, EcuJobResult ecuJobResult)
        {
            List<EcuResultStateValue> ecuResultStateValueList = new List<EcuResultStateValue>();
            string sql = string.Format(@"SELECT ID, TITLE_ENUS, TITLE_DEDE, TITLE_RU, STATEVALUE, VALIDFROM, VALIDTO, PARENTID " +
                                       "FROM XEP_STATEVALUES WHERE (PARENTID IN (SELECT STATELISTID FROM XEP_REFSTATELISTS WHERE (ID = {0})))", ecuJobResult.Id);
            SQLiteCommand command = new SQLiteCommand(sql, mDbConnection);
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    ecuResultStateValueList.Add(new EcuResultStateValue(reader["ID"].ToString(),
                        reader["TITLE_ENUS"].ToString(),
                        reader["TITLE_DEDE"].ToString(),
                        reader["TITLE_RU"].ToString(),
                        reader["STATEVALUE"].ToString(),
                        reader["VALIDFROM"].ToString(),
                        reader["VALIDTO"].ToString(),
                        reader["PARENTID"].ToString()));
                }
            }

            return ecuResultStateValueList;
        }

        private static List<EcuFixedFuncStruct> GetEcuFixedFuncStructList(SQLiteConnection mDbConnection, string parentId)
        {
            List<EcuFixedFuncStruct> ecuFixedFuncStructList = new List<EcuFixedFuncStruct>();
            string sql = string.Format(@"SELECT ID, NODECLASS, TITLE_ENUS, TITLE_DEDE, TITLE_RU, " +
                                       "PREPARINGOPERATORTEXT_ENUS, PREPARINGOPERATORTEXT_DEDE, PREPARINGOPERATORTEXT_RU, " +
                                       "PROCESSINGOPERATORTEXT_ENUS, PROCESSINGOPERATORTEXT_DEDE, PROCESSINGOPERATORTEXT_RU, " +
                                       "POSTOPERATORTEXT_ENUS, POSTOPERATORTEXT_DEDE, POSTOPERATORTEXT_RU " +
                                       "FROM XEP_ECUFIXEDFUNCTIONS WHERE (PARENTID = {0})", parentId);
            SQLiteCommand command = new SQLiteCommand(sql, mDbConnection);
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string nodeClass = reader["NODECLASS"].ToString();
                    EcuFixedFuncStruct ecuFixedFuncStruct = new EcuFixedFuncStruct(reader["ID"].ToString(),
                        nodeClass,
                        GetNodeClassName(mDbConnection, nodeClass),
                        reader["TITLE_ENUS"].ToString(),
                        reader["TITLE_DEDE"].ToString(),
                        reader["TITLE_RU"].ToString(),
                        reader["PREPARINGOPERATORTEXT_ENUS"].ToString(),
                        reader["PREPARINGOPERATORTEXT_DEDE"].ToString(),
                        reader["PREPARINGOPERATORTEXT_RU"].ToString(),
                        reader["PROCESSINGOPERATORTEXT_ENUS"].ToString(),
                        reader["PROCESSINGOPERATORTEXT_DEDE"].ToString(),
                        reader["PROCESSINGOPERATORTEXT_RU"].ToString(),
                        reader["POSTOPERATORTEXT_ENUS"].ToString(),
                        reader["POSTOPERATORTEXT_DEDE"].ToString(),
                        reader["POSTOPERATORTEXT_RU"].ToString());

                    ecuFixedFuncStruct.EcuJobList = GetFixedFuncStructJobsList(mDbConnection, ecuFixedFuncStruct);
                    ecuFixedFuncStructList.Add(ecuFixedFuncStruct);
                }
            }

            return ecuFixedFuncStructList;
        }

        private static EcuVariant GetEcuVariantFunctions(TextWriter outTextWriter, SQLiteConnection mDbConnection, string ecuName)
        {
            EcuVariant ecuVariant = GetEcuVariant(mDbConnection, ecuName);
            if (ecuVariant == null)
            {
                outTextWriter?.WriteLine("ECU variant not found");
                return null;
            }

            List<RefEcuVariant> refEcuVariantList = new List<RefEcuVariant>();
            {
                string sql = string.Format(@"SELECT ID, ECUVARIANTID FROM XEP_REFECUVARIANTS WHERE ECUVARIANTID = {0}", ecuVariant.Id);
                SQLiteCommand command = new SQLiteCommand(sql, mDbConnection);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        refEcuVariantList.Add(new RefEcuVariant(reader["ID"].ToString(),
                            reader["ECUVARIANTID"].ToString()));
                    }
                }
            }

            if (refEcuVariantList.Count == 0)
            {
                outTextWriter?.WriteLine("Ref ECU var functions not found");
                return null;
            }

            ecuVariant.RefEcuVariantList = refEcuVariantList;

            foreach (RefEcuVariant refEcuVariant in refEcuVariantList)
            {
                List<EcuFixedFuncStruct> ecuFixedFuncStructList = GetEcuFixedFuncStructList(mDbConnection, refEcuVariant.Id);
                if (ecuFixedFuncStructList.Count == 0)
                {
                    outTextWriter?.WriteLine("ECU fixed function structures not found for ref ECU var");
                    return null;
                }

                refEcuVariant.FixedFuncStructList = ecuFixedFuncStructList;
            }

            List<EcuVarFunc> ecuVarFunctionsList = new List<EcuVarFunc>();
            foreach (string ecuGroupFunctionId in ecuVariant.GroupFunctionIds)
            {
                string sql = string.Format(@"SELECT ID, VISIBLE, NAME, OBD_RELEVANZ FROM XEP_ECUVARFUNCTIONS WHERE (lower(NAME) = '{0}') AND (ECUGROUPFUNCTIONID = {1})", ecuName.ToLowerInvariant(), ecuGroupFunctionId);
                SQLiteCommand command = new SQLiteCommand(sql, mDbConnection);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ecuVarFunctionsList.Add(new EcuVarFunc(reader["ID"].ToString(), ecuGroupFunctionId));
                    }
                }
            }

            if (ecuVarFunctionsList.Count == 0)
            {
                outTextWriter?.WriteLine("ECU var functions not found");
                return null;
            }

            foreach (EcuVarFunc ecuVarFunc in ecuVarFunctionsList)
            {
                outTextWriter?.WriteLine(ecuVarFunc);
            }

            List<EcuFuncStruct> ecuFuncStructList = new List<EcuFuncStruct>();
            foreach (EcuVarFunc ecuVarFunc in ecuVarFunctionsList)
            {
                string sql = string.Format(@"SELECT REFFUNCS.ECUFUNCSTRUCTID FUNCSTRUCTID, TITLE_ENUS, TITLE_DEDE, TITLE_RU " +
                        "FROM XEP_ECUFUNCSTRUCTURES FUNCS, XEP_REFECUFUNCSTRUCTS REFFUNCS WHERE FUNCS.ID = REFFUNCS.ECUFUNCSTRUCTID AND REFFUNCS.ID = {0}", ecuVarFunc.Id);
                SQLiteCommand command = new SQLiteCommand(sql, mDbConnection);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ecuFuncStructList.Add(new EcuFuncStruct(reader["FUNCSTRUCTID"].ToString(),
                            reader["TITLE_ENUS"].ToString(),
                            reader["TITLE_DEDE"].ToString(),
                            reader["TITLE_RU"].ToString()));
                    }
                }
            }

            if (ecuFuncStructList.Count == 0)
            {
                outTextWriter?.WriteLine("ECU function structures not found");
                return null;
            }

            foreach (EcuFuncStruct ecuFuncStruct in ecuFuncStructList)
            {
                List<EcuFixedFuncStruct> ecuFixedFuncStructList = GetEcuFixedFuncStructList(mDbConnection, ecuFuncStruct.Id);
                if (ecuFixedFuncStructList.Count == 0)
                {
                    outTextWriter?.WriteLine("ECU fixed function structures not found for ECU func struct");
                    return null;
                }

                ecuFuncStruct.FixedFuncStructList = ecuFixedFuncStructList;
            }

            ecuVariant.EcuFuncStructList = ecuFuncStructList;

            return ecuVariant;
        }

        // ReSharper disable once UnusedMember.Local
        private static bool CreateZip(List<string> inputFiles, string archiveFilenameOut)
        {
            try
            {
                if (File.Exists(archiveFilenameOut))
                {
                    File.Delete(archiveFilenameOut);
                }
                FileStream fsOut = File.Create(archiveFilenameOut);
                ZipOutputStream zipStream = new ZipOutputStream(fsOut);
                zipStream.SetLevel(3);

                try
                {
                    foreach (string filename in inputFiles)
                    {

                        FileInfo fi = new FileInfo(filename);
                        string entryName = Path.GetFileName(filename);

                        ZipEntry newEntry = new ZipEntry(entryName)
                        {
                            DateTime = fi.LastWriteTime,
                            Size = fi.Length
                        };
                        zipStream.PutNextEntry(newEntry);

                        byte[] buffer = new byte[4096];
                        using (FileStream streamReader = File.OpenRead(filename))
                        {
                            StreamUtils.Copy(streamReader, zipStream, buffer);
                        }
                        zipStream.CloseEntry();
                    }
                }
                finally
                {
                    zipStream.IsStreamOwner = true;
                    zipStream.Close();
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
