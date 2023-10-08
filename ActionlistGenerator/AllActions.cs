using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Xml.Linq;

namespace ActionlistGenerator
{
    public class AllActions
    {
        private static Action callRfc = new Action("callRFC", "Führt eine RFC-Anfrage durch", new XElement("callRfc",
            new XElement("functionName"),
            new XElement("outputFile"),
            new XElement("outputProperty"),
            new XElement("params", new XElement("parameter"), new XElement("parameter")),
            new XElement("sapConnectConfigFile"),
            new XElement("useExportParameters"),
            new XElement("useTables")

        ));
        private static Action callWebservice = new Action("callWebService", "Führt eine HTTP-Anfrage durch.", new XElement("callWebService",
            new XElement("url"),
            new XElement("userName"),
            new XElement("passwordCrypted"),
            new XElement("cookieProperty"),
            new XElement("outputFile"),
            new XElement("requestParameters",
                new XElement("requestParameter",
                    new XAttribute("name", "someParam"),
                    new XAttribute("value", "some value")
                ),
                new XElement("requestParameter",
                    new XAttribute("name", "someOtherParam"),
                    new XAttribute("value", "some other value")
                )
            ),
            new XElement("httpHeaders",
                new XElement("header",
                    new XAttribute("name", "someParam"),
                    new XAttribute("value", "some value")
                ),
                new XElement("header",
                    new XAttribute("name", "someOtherParam"),
                    new XAttribute("value", "some other value")
                )
            ),
            new XElement("ignoreError"),
            new XElement("inputFile"),
            new XElement("altSpaceEnc"),
            new XElement("inputProperty"),
            new XElement("method"),
            new XElement("outputProperty")
        ));
        private static Action choose = new Action("choose", "Quasi eine if-Abfrage.", new XElement("choose",
            new XElement("when", 
                new XAttribute("test", "count(//item) > 0"),
                new XAttribute("file", "VMWORK/temp/somefile.xml")
            ),
            new XElement("otherwise")
        ));
        private static Action cleanupEpim = new Action("cleanupEPIM", "Löscht diverse Datenbankeinträge der EPIM Datenbank.", new XElement("cleanupEPIM",
            new XAttribute("type", "csvExport"),
            new XAttribute("id", "00000")
        ));
        private static Action config = new Action("config", "Liest Variablen aus/schreibt Variablen in Konfig-Dateien.", new XElement("config",
            new XAttribute("action", "load"),
            new XAttribute("file", "${processConfigFile}"),
            new XAttribute("mode", "${properties}"),
            new XAttribute("propertyList", "propert1;property2;property3"),
            new XAttribute("propertyName", "someProperty"),
            new XAttribute("overwrite", "true"),
            new XAttribute("handout", "true")
        ));
        private static Action convert = new Action("convert", "Konvertiert Dateien zwischen verschiedenen Formaten, z.B. XML<>JSON.", new XElement("convert",
            new XAttribute("type", "xml2json"),
            new XAttribute("inputFile", "VMWORK/processname/sessionID/somefile.xml"),
            new XAttribute("inputProperty", "xmlProp"),
            new XAttribute("outputFile", "${VMHANDOVER}/out.json")
        ));
        private static Action copy = new Action("copy", "Kopiert Dateien und Ordner.", new XElement("copy",
            new XElement("file", "${VMWORK}/processname/sessionID/out.json"),
            new XElement("targetDir", "${VMWORK}/process"),
            new XElement("targetFilename", "copy.json"),
            new XElement("append", "false"),
            new XElement("ignoreNoFilesFound", "false"),
            new XElement("sourcePattern", "")
        ));
        private static Action createHash = new Action("createHash", "Generiert die Prüfsumme einer Property/Datei.", new XElement("createHash",
            new XAttribute("algorithm", "SHA512"),
            new XAttribute("propertyName", "checksumProperty"),
            new XAttribute("handout", "true"),
            new XAttribute("overwrite", "true"),
            new XAttribute("file", "${VMWORK}/process/input.xml"),
            new XAttribute("value", "content to be hashed")
        ));
        private static Action createList = new Action("createList", "Erstellt eine Dateiliste.", new XElement("createList",
            new XAttribute("listDirs", "false"),
            new XAttribute("includeSubDirs", "false"),
            new XAttribute("pathIsRelative", "false"),
            new XAttribute("ignoreEmptyList", "false"),
            new XElement("sourceDir", "${VMWORK}/generatedFiles"),
            new XElement("listFilePattern", ".*\\.xml"),
            new XElement("outputFile", "${EPIMFS}/produktion/import/out.xml"),
            new XElement("outputProperty", "myListFileProperty")
        ));
        private static Action delete = new Action("delete", "löscht Dateien und Verzeichnisse.", new XElement("delete",
            new XAttribute("dir", "${sessionWorkDir}/temp"),
            new XAttribute("recursive", "false"),
            new XAttribute("pattern", "tempfile_.*\\.xml"),
            new XAttribute("ignoreFilesNotFound", "false")
        ));
        private static Action echo = new Action("echo", "Schreibt Text ins Log oder in eine Datei.", new XElement("echo",
            new XAttribute("message", "Some message"),
            new XAttribute("file", "${sessionWorkDir}/debug.log"),
            new XAttribute("propertyName", "logProperty"),
            new XAttribute("appendFile", "true"),
            new XAttribute("overwrite", "false")
        ));
        private static Action email = new Action("email", "Versendet eine E-mail.", new XElement("email",
            new XAttribute("file", "${processDir}/email/send_password.xml"),
            new XElement("to", "someone@address.com"),
            new XElement("cc", "someone_else@address.com"),
            new XElement("sender", "sender@address.com"),
            new XElement("subject", "Testmail! Wichtig!!"),
            new XElement("contentType", "html"),
            new XElement("body", "Hallo! Dies ist eine Testmal!"),
            new XElement("attachmentFile", "${sessionWorkDir}/out.zip"),
            new XElement("attachmentFile", "${sessionWorkDir}/documentList.xml")
        ));
        private static Action executeShell = new Action("executeShell", "Führt einen Shell-/Systembefehl aus.", new XElement("executeShell",
            new XElement("executable", "myExecutableFile"),
            new XElement("parameter", "-f"),
            new XElement("parameter", "somefile.txt"),
            new XElement("parameter", "-o"),
            new XElement("parameter", "${VMWORK}/output.xml"),
            new XElement("workDir", "${workDir}"),
            new XElement("stdOutTo", "${sessionWorkDir}/stdOut.log"),
            new XElement("stdErrTo", 
                new XAttribute("overwrite", "true"),
                new XAttribute("propertyName", "stdErr")
            )
        ));
        private static Action executeSQL = new Action("executeSQL", "Führt eine SQL-Query durch.", new XElement("executeSQL",
            
            new XElement("outputFile", ""),
            new XElement("outputProperty", ""),
            new XElement("sql", "")
        ));        
        private static Action executeXQuery = new Action("executeXQuery", "Führt Anfragen in einer XML-Datenbank aus.", new XElement("executeXQuery",
            new XAttribute("xmlDB", "basex"),
            new XAttribute("dbName", "books"),
            new XElement("command", "command content"),
            new XElement("queryFile", "${workDir}/myXQuery.xml"),
            new XElement("param", "some value"),
            new XElement("outputFile", "${VMHANDOVER}/myXQueryOutput.xml")
        ));
        private static Action exit = new Action("exit", "beendet die Actionlist ohne Fehler.", new XElement("exit",
            new XAttribute("message", "A relevant exit message")
        ));
        private static Action fail = new Action("fail", "Bricht die Actionlist mit einer Fehlermeldung ab.", new XElement("fail",
            new XAttribute("message", "some error message")
        ));
        private static Action fop = new Action("fop", "Erstellt PDF-Dateien mittels Apache FOP aus einer FO-Datei.", new XElement("fop",
            new XAttribute("logToFile", "false"),
            new XElement("inputFile", "${workDir}/RNG_01234.fo"),
            new XElement("outputFile", "${workDir}/RNG_01234_out.pdf"),
            new XElement("mimeType", "application/pdf")
        ));
        private static Action forEachItemListXml = new Action("forEachItemListXml", "Iteriert über eine Itemlist.", new XElement("forEachItemListXml",
            new XAttribute("file", "${sessionWorkDir}/products.itemlist.xml"),
            new XAttribute("ignoreErrorsInIteration", "false")
        ));
        private static Action forEachKeyValueList = new Action("forEachKeyValueList", "Iteriert über Werte einer Schlüssel-Wert-Liste.", new XElement("forEachKeyValueList",
            new XAttribute("file", "${sessionWorkDir}/products.itemlist.xml"),
            new XAttribute("ignoreErrorsInIteration", "false")
        ));
        private static Action forEachList = new Action("forEachList", "Iteriert anhand eines Trennzeichens über eine Liste von Elementen.", new XElement("forEachList",
            new XAttribute("list", "value1;value2;value3"),
            new XAttribute("delimier", ";"),
            new XAttribute("ignoreErrorsInIteration", "false"),
            new XAttribute("propertyName", "listProperty")
        ));
        private static Action getDataFromSap = new Action("getDataFromSap", "Importiert Produkte von SAP zu EPIM.", new XElement("getDataFromSap",
            new XElement("sapConnectConfigFile", "${workDir}/sapConn.xml"),
            new XElement("runMode", "multiFile|singleFile|epim"),
            new XElement("rfcConfigFile", "${workDir}/rfcConn.xml"),
            new XElement("epimSelectStatement", "some select statement"),
            new XElement("outputDir", "${sessionWorkDir}/result.xml"),
            new XElement("outputToProperty", "false"),
            new XElement("postProcessingActionList", "${actionlistDir}/000_some-steps.actionlist.xml"),
            new XElement("cleanupActionList", "${actionlistDir}/000_cleanup.actionlist.xml"),
            new XElement("maxMaterialNumbers", "1000"),
            new XElement("testMaterialNumbers", "1,3,666")
        ));
        private static Action milestone = new Action("milestone", "Speichert den Status eines Prozess, sodass er später fortgeführt werden kann.", new XElement("milestone", 
            new XAttribute("name", "name of the milstone")
        ));
        private static Action mkDir = new Action("mkDir", "Erstellt ein Verzeichnis, ggfs. rekursiv.", new XElement("mkDir",
            new XAttribute("dir", "${sessionWorkDir}/generated-files")
        ));
        private static Action move = new Action("move", "Verschiebt Dateien.", new XElement("move",
            new XAttribute("overwriteTarget", "true"),
            new XElement("file", "${VMWORK}/somefile.txt"),
            new XElement("targetDir", "${VMHANDOVER}"),
            new XElement("targetFileName", "movedFile.txt")
        ));
        private static Action property = new Action("property", "Deklariert/setzt eine Variable.", new XElement("move",
            new XAttribute("name", "myProperty"),
            new XAttribute("value", "my property value"),
            new XAttribute("file", "${VMWORK}/sourceFile.txt"),
            new XAttribute("overwrite", "true"),
            new XAttribute("handout", "true"),
            new XAttribute("xpath", "count(//item)"),
            new XAttribute("xpathSourceProperty", "sourceProp")
        ));
        private static Action rename = new Action("rename", "Benennt Dateien und Verzeichniss um.", new XElement("rename",
            new XElement("sourcePath", "${workDir}/important-file.xml"),
            new XElement("targetName", "newname.xml")
        ));
        private static Action searchAndReplace = new Action("searchReplace", "Ersetzt Daten in Dateien.", new XElement("searchandreplace",
            new XAttribute("encoding", "utf-8"),
            new XElement("sourceFile", "important-file.xml"),
            new XElement("targetFile", "${sessionWorkDir}/replaced.xml"),
            new XElement("searchReplacePair")
        ));
        private static Action setOrderBack = new Action("setOrderBack", "Setzt eine JobScheduler Order zurück.", new XElement("setOrderBack"));
        private static Action setOrderState = new Action("setOrderState", "Setzen den Status der Order.", new XElement("setOrderState",
            new XAttribute("value", "200")
        ));
        private static Action sleep = new Action("sleep", "Blockiert die Ausführung für die angegebene Zeitdauer.", new XElement("sleep",
            new XAttribute("hours", "0"),
            new XAttribute("minutes", "0"),
            new XAttribute("seconds", "0"),
            new XAttribute("milliseconds", "0")
        ));
        private static Action touch = new Action("touch", "Erstellt eine leere Datei", new XElement("touch",
            new XAttribute("file", "${exportDir}/export.xml")
        ));
        private static Action transform = new Action("transform", "Führt eine XSL-Transformation durch.", new XElement("transform",
            new XAttribute("indent", "true"),
            new XElement("inputFile", "${sessionWorkDir}/input.xml"),
            new XElement("inputProperty", "someProperty"),
            new XElement("xslProcessor", "saxon2"),
            new XElement("stylesheet", "${xslDir}/my-transformation.xsl"),
            new XElement("outputFile", "${sessionWorkDir}/output.xml"),
            new XElement("outputProperty", "outProperty"),
            new XElement("param",
                new XAttribute("name", "paramName1"),
                new XAttribute("value", "some custom value 1")
            ),
            new XElement("param",
                new XAttribute("name", "paramName2"),
                new XAttribute("value", "some custom value 2")
            )
        ));
        private static Action unzip = new Action("unzip", "Entpackt eine Zip-Datei.", new XElement("unzip",
            new XAttribute("ignoreNoFilesFound", "false"),
            new XElement("file", "${sessionBackupDir}/input.zip"),
            new XElement("targetBaseDir", "${sessionWorkDir}"),
            new XElement("targetDirName", "input")
        ));
        private static Action validateXml = new Action("validateXML", "Validiert eine XML-Datei gegen ein XSD-Schema.", new XElement("validateXML",
            new XAttribute("failOnError", "true"),
            new XElement("inputFile", "${sessionWorkDir}/created.xml"),
            new XElement("inputProperty", "inputProp"),
            new XElement("schemaFile", "${xslDir}/schema.xsd"),
            new XElement("logFile", "${sessionWorkDir}/validate-xml.log"),
            new XElement("logProperty", "outputProp")
        ));
        private static Action zip = new Action("zip", "Erstellt eine Zip-Datei aus Dateien und Verzeichnissen.", new XElement("zip",
            new XAttribute("ignoreNoFilesFound", "false"),
            new XElement("sourceFolder", "${langWorkDir}/toBeZipped"),
            new XElement("sourcePattern", ".*"),
            new XElement("targetDir", "${langWorkDir}"),
            new XElement("targetFileName", "langBackup"),
            new XElement("sourceDir", "${langWorkDir}/toBeZipped"),
            new XElement("file", "${langWorkDir}/eng.txt")
        ));

        private static List<Action> list = new List<Action>() 
        { 
            callRfc,
            callWebservice,
            choose,
            cleanupEpim,
            config,
            convert,
            copy,
            createHash,
            createList,
            delete,
            echo,
            email,
            executeShell,
            executeSQL,
            executeXQuery,
            exit,
            fail,
            fop,
            forEachItemListXml,
            forEachKeyValueList,
            forEachList,
            getDataFromSap,
            milestone,
            mkDir,
            move,
            property,
            rename,
            searchAndReplace,
            setOrderBack,
            setOrderState,
            sleep,
            touch,
            transform,
            unzip,
            validateXml,
            zip,
        };

        public static List<Action> ListOfAvailableActions => list;
    }
}
