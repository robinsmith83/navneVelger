using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace NavneVelger.Views.Panini
{
    public static class ManagePaniniPages
    {
        public static string ActivePageKey => "ActivePage";

        public static string Index => "Index";

        public static string Eier => "Eier";

        public static string DeleteEier => "DeleteEier";
        public static string DeleteType => "DeleteType";
        public static string DeleteBok => "DeleteBok";

        public static string Type => "Type";

        public static string Merker => "Merker";

        public static string Klistremerkebok => "Klistremerkebok";
        public static string BokInfo => "BokInfo";
        public static string LeggTilBok => "LeggTilBok";

        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        public static string EierNavClass(ViewContext viewContext) => PageNavClass(viewContext, Eier);

        public static string DeleteEierNavClass(ViewContext viewContext) => PageNavClass(viewContext, DeleteEier);
        public static string DeleteTypeNavClass(ViewContext viewContext) => PageNavClass(viewContext, DeleteType);
        public static string DeleteBokNavClass(ViewContext viewContext) => PageNavClass(viewContext, DeleteBok);
        public static string LeggTilBokNavClass(ViewContext viewContext) => PageNavClass(viewContext, LeggTilBok);
        public static string BokInfoNavClass(ViewContext viewContext) => PageNavClass(viewContext, BokInfo);

        public static string TypeNavClass(ViewContext viewContext) => PageNavClass(viewContext, Type);

        public static string MerkerNavClass(ViewContext viewContext) => PageNavClass(viewContext, Merker);

        public static string KlistremerkebokNavClass(ViewContext viewContext) => PageNavClass(viewContext, Klistremerkebok);
    
        public static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string;
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }

        public static void AddActivePaniniPage(this ViewDataDictionary viewData, string activePage) => viewData[ActivePageKey] = activePage;
    }
}
