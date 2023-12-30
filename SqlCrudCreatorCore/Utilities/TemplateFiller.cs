using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlCrudCreatorCore.Utilities
{
    public class TemplateFiller
    {
        /// <summary>
        /// Replaces place holder text with values in the Replacers prop. 
        /// </summary>
        /// <param name="templateFillerParms"></param>
        /// <returns></returns>
        public static string FillClassObjectTemplate(TemplateFillerParms templateFillerParms)
        {
            foreach(var templateFiller in templateFillerParms.Replacers)
            {
                templateFillerParms.InputText = templateFillerParms.InputText.Replace(templateFiller.Key, templateFiller.Value);
            }
           
            return templateFillerParms.InputText;
        }
    }

    public class TemplateFillerParms
    {
        public string InputText { get; set; }

        public Dictionary<string, string> Replacers { get; set; }
    
    }

}
