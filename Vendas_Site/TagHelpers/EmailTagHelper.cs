using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Vendas_Site.TagHelpers
{
    public class EmailTagHelper : TagHelper
    {
        public string Endereço { get; set; }
        public string Conteudo { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.Attributes.SetAttribute("href", "mailto:" + Endereço);
            output.Content.SetContent(Conteudo);
    }
    }
}
