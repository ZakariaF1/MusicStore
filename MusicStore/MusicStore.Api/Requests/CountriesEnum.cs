using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace MusicStore.Api.Requests
{
    public class CountriesEnum
    {
        public enum Countries
        {
            Afghanistan,
            Albania,
            Algeria,
            Andorra,
            Angola,
            [Description("Antigua and Barbuda")]
            AntiguaAndBarbuda,
            Argentina,
            Armenia,
            Australia,
            Austria,
            Azerbaijan,
            [Description("The Bahamas")]
            TheBahamas,
            Bahrain,
            Bangladesh,
            Barbados,
            Belarus,
            Belgium,
            Belize,
            Benin,
            Bhutan,
            Bolivia,
            [Description("Bosnia and Herzegovina")]
            BosniaAndHerzegovina,
            Botswana,
            Brazil,
            Brunei,
            Bulgaria,
            [Description("Burkina Faso")]
            BurkinaFaso,
            Burundi,
            Advertisement,
            [Description("Cabo Verde")]
            CaboVerde,
            Cambodia,
            Cameroon,
            Canada,
            [Description("Central African Republic")]
            CentralAfricanRepublic,
            Chad,
            Chile,
            China,
            Colombia,
            Comoros,
            Congo,
            [Description("Côte d’Ivoire")]
            CôtedIvoire,
            Croatia,
            Cuba,
            Cyprus,
            [Description("Czech Republic")]
            CzechRepublic,
            Denmark,
            Djibouti,
            Dominica,
            [Description("Dominican Republic")]
            DominicanRepublic,
            [Description("East Timor (Timor-Leste)")]
            EastTimorTimorLeste,
            Ecuador,
            Egypt,
            [Description("El Salvador")]
            ElSalvador,
            [Description("Equatorial Guinea")]
            EquatorialGuinea,
            Eritrea,
            Estonia,
            Ethiopia,
            Fiji,
            Finland,
            France,
            Gabon,
            [Description("The Gambia")]
            TheGambia,
            Georgia,
            Germany,
            Ghana,
            Greece,
            Grenada,
            Guatemala,
            Guinea,
            [Description("Guinea-Bissau")]
            GuineaBissau,
            Guyana,
            Haiti,
            Honduras,
            Hungary,
            Iceland,
            India,
            Indonesia,
            Iran,
            Iraq,
            Ireland,
            Israel,
            Italy,
            Jamaica,
            Japan,
            Jordan,
            Kazakhstan,
            Kenya,
            Kiribati,
            [Description("Korea North")]
            KoreaNorth,
            [Description("Korea South")]
            KoreaSouth,
            Kosovo,
            Kuwait,
            Kyrgyzstan,
            Laos,
            Latvia,
            Lebanon,
            Lesotho,
            Liberia,
            Libya,
            Liechtenstein,
            Lithuania,
            Luxembourg,
            Macedonia,
            Madagascar,
            Malawi,
            Malaysia,
            Maldives,
            Mali,
            Malta,
            [Description("Marshall Islands")]
            MarshallIslands,
            Mauritania,
            Mauritius,
            Mexico,
            Micronesia,
            Moldova,
            Monaco,
            Mongolia,
            Montenegro,
            Morocco,
            Mozambique,
            Myanmar,
            Namibia,
            Nauru,
            Nepal,
            Netherlands,
            [Description("New Zealand")]
            NewZealand,
            Nicaragua,
            Niger,
            Nigeria,
            Norway,
            Oman,
            Pakistan,
            Palau,
            Panama,
            [Description("Papua New Guinea")]
            PapuaNewGuinea,
            Paraguay,
            Peru,
            Philippines,
            Poland,
            Portugal,
            Qatar,
            Romania,
            Russia,
            Rwanda,
            [Description("Saint Kitts and Nevis")]
            SaintKittsandNevis,
            [Description("Saint Lucia")]
            SaintLucia,
            [Description("Saint Vincent and the Grenadines")]
            SaintVincentandtheGrenadines,
            Samoa,
            [Description("San Marino")]
            SanMarino,
            [Description("Sao Tome and Principe")]
            SaoTomeandPrincipe,
            [Description("Saudi Arabia")]
            SaudiArabia,
            Senegal,
            Serbia,
            Seychelles,
            [Description("Sierra Leone")]
            SierraLeone,
            Singapore,
            Slovakia,
            Slovenia,
            [Description("Solomon Islands")]
            SolomonIslands,
            Somalia,
            [Description("South Africa")]
            SouthAfrica,
            Spain,
            [Description("Sri Lanka")]
            SriLanka,
            Sudan,
            [Description("Sudan South")]
            SudanSouth,
            Suriname,
            Swaziland,
            Sweden,
            Switzerland,
            Syria,
            Taiwan,
            Tajikistan,
            Tanzania,
            Thailand,
            Togo,
            Tonga,
            [Description("Trinidad and Tobago")]
            TrinidadandTobago,
            Tunisia,
            Turkey,
            Turkmenistan,
            Tuvalu,
            Uganda,
            Ukraine,
            [Description("United Arab Emirates")]
            UnitedArabEmirates,
            [Description("United-Kingdom")]
            UnitedKingdom,
            [Description("United-States")]
            UnitedStates,
            Uruguay,
            Uzbekistan,
            Vanuatu,
            [Description("Vatican City")]
            VaticanCity,
            Venezuela,
            Vietnam,
            Yemen,
            Zambia,
            Zimbabwe
        }
        public static string StringValueOfEnum(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }
    }
}
