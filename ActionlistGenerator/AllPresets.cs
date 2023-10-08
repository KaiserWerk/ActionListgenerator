using System.Collections.Generic;
using System.Xml.Linq;

namespace ActionlistGenerator
{
    public class AllPresets
    {
        public static Preset xmlImport = new Preset("XML Import", "Erstellt die Elemente für einen XML-Import", new() { 
            new XElement("echo",
                new XAttribute("message", "Logging into XMLImport service")
            ),
            new XElement("callWebService",
                new XElement("method", "POST"),
                new XElement("url", "${EPIM_WebServices_BaseURL}/XMLImport/login"),
                new XElement("userName", "${EPIM_WebServices_User}"),
                new XElement("passwordCrypted", "${EPIM_WebServices_PasswordCrypted}"),
                new XElement("outputFile", "${sessionWorkDir}/import-login-response.xml"),
                new XElement("cookieProperty", "import_main")
            ),
            new XElement("callWebService",
                new XElement("method", "POST"),
                new XElement("url", "${EPIM_WebServices_BaseURL}/XMLImport/startImport"),
                new XElement("outputFile", "${sessionWorkDir}/start-import-response.xml"),
                new XElement("cookieProperty", "import_main"),
                new XElement("requestOarameters",
                    new XElement("requestParameter",
                        new XAttribute("name", "p_sImportFile"),
                        new XAttribute("value", "${processImportDir}/import-file.xml")
                    ),
                    new XElement("requestParameter",
                        new XAttribute("name", "p_sConfigFile"),
                        new XAttribute("value", "${EPIMIMPORT}/profiles/${importProfile}")
                    ),
                    new XElement("requestParameter",
                        new XAttribute("name", "p_sImportLanguageId"),
                        new XAttribute("value", "2")
                    ),
                    new XElement("requestParameter",
                        new XAttribute("name", "p_sPratImportMode"),
                        new XAttribute("value", "1")
                    ),
                    new XElement("requestParameter",
                        new XAttribute("name", "p_sCoatImportMode"),
                        new XAttribute("value", "1")
                    ),
                    new XElement("requestParameter",
                        new XAttribute("name", "p_sCheckTimestamp"),
                        new XAttribute("value", "0")
                    ),
                    new XElement("requestParameter",
                        new XAttribute("name", "p_sIgnoreMustAttr"),
                        new XAttribute("value", "0")
                    ),
                    new XElement("requestParameter",
                        new XAttribute("name", "p_sViaCONTENT"),
                        new XAttribute("value", "2")
                    ),
                    new XElement("requestParameter",
                        new XAttribute("name", "p_sViaPRODUCT"),
                        new XAttribute("value", "0")
                    ),
                    new XElement("requestParameter",
                        new XAttribute("name", "p_sViaDICT"),
                        new XAttribute("value", "0")
                    ),
                    new XElement("requestParameter",
                        new XAttribute("name", "p_sReimportUnformDict"),
                        new XAttribute("value", "0")
                    )
                )
            ),
        });
        private static Preset xmlImportCheck = new Preset("XML Import Statusprüfung", "Eine umfassende Statusprüfung eines angestoßenen XML-Imports.", new() {
            new XElement("echo", new XAttribute("message", "Starting XML import check")),
            new XElement("executeSQL", 
                new XAttribute("outputFormat", "itemlist"),
                new XElement("outputFile", "${sessionWorkDir}/import-status.xml"),
                new XElement("sql", "SELECT ID, STATUS, ELEMENT_STATE, FILENAME FROM XMLIMPORTS WHERE FILENAME LIKE '%${processName}%${sessionId}%'")
            ),
            new XElement("property", 
                new XAttribute("name", "importCountTotal"),
                new XAttribute("file", "${sessionWorkDir}/import-status.xml"),
                new XAttribute("xpath", "count(//item)"),
                new XAttribute("overwrite", "true"),
                new XAttribute("handout", "true")
            ),
            new XElement("property",
                new XAttribute("name", "elementCountTotal"),
                new XAttribute("file", "${sessionWorkDir}/import-status.xml"),
                new XAttribute("xpath", "count(//item[@element_state != 'null'])"),
                new XAttribute("overwrite", "true"),
                new XAttribute("handout", "true")
            ),
            new XElement("property",
                new XAttribute("name", "importCountSuccess"),
                new XAttribute("file", "${sessionWorkDir}/import-status.xml"),
                new XAttribute("xpath", "count(//item[@status='F'])"),
                new XAttribute("overwrite", "true"),
                new XAttribute("handout", "true")
            ),
            new XElement("property",
                new XAttribute("name", "elementCountSuccess"),
                new XAttribute("file", "${sessionWorkDir}/import-status.xml"),
                new XAttribute("xpath", "count(//item[@element_state='E'])"),
                new XAttribute("overwrite", "true"),
                new XAttribute("handout", "true")
            ),
            new XElement("property",
                new XAttribute("name", "importCountError"),
                new XAttribute("file", "${sessionWorkDir}/import-status.xml"),
                new XAttribute("xpath", "count(//item[@status='E'])"),
                new XAttribute("overwrite", "true"),
                new XAttribute("handout", "true")
            ),
            new XElement("property",
                new XAttribute("name", "elementCountError"),
                new XAttribute("file", "${sessionWorkDir}/import-status.xml"),
                new XAttribute("xpath", "count(//item[@element_status='F' or @element_state='A'])"),
                new XAttribute("overwrite", "true"),
                new XAttribute("handout", "true")
            ),
            new XElement("property",
                new XAttribute("name", "importCountFinished"),
                new XAttribute("file", "${sessionWorkDir}/import-status.xml"),
                new XAttribute("xpath", "count(//item[@status='F' or @status='E' or @status='A'])"),
                new XAttribute("overwrite", "true"),
                new XAttribute("handout", "true")
            ),
            new XElement("property",
                new XAttribute("name", "elementCountFinished"),
                new XAttribute("file", "${sessionWorkDir}/import-status.xml"),
                new XAttribute("xpath", "count(//item[@element_state='F' or @element_state='A' or @element_state='E'])"),
                new XAttribute("overwrite", "true"),
                new XAttribute("handout", "true")
            ),
            new XElement("choose",
                new XElement("when",
                    new XAttribute("test", "${importCountTotal} = ${importCountSuccess}"),
                    new XElement("echo", new XAttribute("message", "${importCountSuccess} finished successfully.")),
                    new XElement("choose",
                        new XElement("when", new XAttribute("test", "${elementCountTotal} = ${elementCountSuccess}"),
                            new XElement("echo", new XAttribute("message", "${elementCountSuccess} finished successfully."))
                        ),
                        new XElement("when", new XAttribute("test", "${elementCountTotal} = ${elementCountSuccess} + ${elementCountError} and ${elementCountError > 0"),
                            new XElement("echo", new XAttribute("message", "${elementCountSuccess} finished successfully, ${elementCountError} with error. Please check the XML import log."))
                        ),
                        new XElement("otherwise",
                             new XElement("echo", new XAttribute("message", "Elements not finished yet. Finished already: (${elementCountFinished}/${elementCountTotal}).")),
                             new XElement("setOrderBack")
                        )
                    )
                ),
                new XElement("when",
                    new XAttribute("test", "${importCountTotal} = ${importCountSuccess} + ${importCountError} and ${importCountError} > 0"),
                    new XElement("echo", new XAttribute("message", "${importCountSuccess} finished successfully, ${importCountError} with error. Please check the XML import log."))
                ),
                new XElement("otherwise",
                    new XElement("echo", new XAttribute("message", "Imports not finished yet. Finished already: (${importCountFinished}/${importCountTotal}).")),
                    new XElement("setOrderBack")
                )
            ),
            new XElement("choose",
                new XElement("when",
                    new XAttribute("test", "${importCountError} > 0"),
                    new XElement("property",
                        new XAttribute("name", "stateProcess"),
                        new XAttribute("value", "ERROR"),
                        new XAttribute("overwrite", "true"),
                        new XAttribute("handout", "true")
                    ),
                    new XElement("echo", new XAttribute("message", "${importCountError} errors during session.")),
                    new XElement("fail", new XAttribute("message", "Errors during session"))
                )
            )
        });

        public static List<Preset> list = new List<Preset>()
        {
            xmlImport,
            xmlImportCheck,
        };

        public static List<Preset> ListOfAvailablePresets => list;
    }
}
