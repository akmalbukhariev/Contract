using LibContract.HttpModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAPI
{
    public class CreateClauses
    {
        public void Create()
        {
            string strJson = "";
            strJson = Normal();
            //strJson = Popular();
            //strJson = Simple();
        }

        private string Normal()
        {
            List<ContractTemplateJson> jList = new List<ContractTemplateJson>();

            ContractTemplateJson item1 = new ContractTemplateJson();
            item1.Title = "1";
            item1.Description = "ШАРТНОМАНИНГ МАЗМУНИ ВА ИШЛАРНИНГ НАРХИ";

            ContractTemplateJson item1_1 = new ContractTemplateJson();
            item1_1.Title = "1.1";
            item1_1.Description = "“Буюртмачи” топшириғига кўра “Бажарувчи” ушбу шартномада белгиланган тартиб ва шартлар асосида қуйидаги таснифда кўрсатилган ишларни бажариш мажбуриятларини ўз зиммасига олади.";
            item1.Child.Add(item1_1);
            jList.Add(item1);

            ContractTemplateJson itemButton1 = new ContractTemplateJson();
            itemButton1.IsButton = true;
            itemButton1.IsContractInfoButton = false;
            itemButton1.IsContractServiceDetailsButton = true;
            itemButton1.IsVisibleAddButton = true;
            jList.Add(itemButton1);

            ContractTemplateJson item2 = new ContractTemplateJson();
            item2.Title = "2";
            item2.Description = "ТОМОНЛАРНИНГ ХУҚУҚИЙ МАНЗИЛЛАРИ";
            jList.Add(item2);

            ContractTemplateJson itemButton2 = new ContractTemplateJson();
            itemButton2.IsButton = true;
            itemButton2.IsContractInfoButton = true;
            itemButton2.IsContractServiceDetailsButton = false;
            itemButton2.IsVisibleAddButton = true;
            jList.Add(itemButton2);

            return JsonConvert.SerializeObject(jList);
        }

        private string Popular()
        {
            List<ContractTemplateJson> jList = new List<ContractTemplateJson>();

            ContractTemplateJson item1 = new ContractTemplateJson();
            item1.Title = "1";
            item1.Description = "ШАРТНОМАНИНГ МАЗМУНИ ВА ИШЛАРНИНГ НАРХИ";

            ContractTemplateJson item1_1 = new ContractTemplateJson();
            item1_1.Title = "1.1";
            item1_1.Description = "“Буюртмачи” топшириғига кўра “Бажарувчи” ушбу шартномада белгиланган тартиб ва шартлар асосида қуйидаги таснифда кўрсатилган ишларни бажариш мажбуриятларини ўз зиммасига олади.";
            item1.Child.Add(item1_1);
            jList.Add(item1);

            ContractTemplateJson itemButton1 = new ContractTemplateJson();
            itemButton1.IsButton = true;
            itemButton1.IsContractInfoButton = false;
            itemButton1.IsContractServiceDetailsButton = true;
            itemButton1.IsVisibleAddButton = true;
            jList.Add(itemButton1);


            ContractTemplateJson item2 = new ContractTemplateJson();
            item2.Title = "2";
            item2.Description = "ҲИСОБ-КИТОБ ТАРТИБИ";

            ContractTemplateJson item2_1 = new ContractTemplateJson();
            item2_1.Title = "2.1";
            item2_1.Description = "Ушбу шартнома кучга киргач, “Буюртмачи” 10 календарь куни мобайнида шартнома баҳосининг 100% миқдоридаги тўловни “Бажарувчи”нинг ҳисоб-рақамига пул ўтказиш мажбуриятини олади.";
            item2.Child.Add(item2_1);
            jList.Add(item2);


            ContractTemplateJson item3 = new ContractTemplateJson();
            item3.Title = "3";
            item3.Description = "ИШЛАРНИ БАЖАРИШ, ТОПШИРИШ ВА ҚАБУЛ ҚИЛИШ ТАРТИБИ";

            ContractTemplateJson item3_1 = new ContractTemplateJson();
            item3_1.Title = "3.1";
            item3_1.Description = "Шартноманинг 2.1. банди бажарилгандан сўнг, “Буюртмачи” 3 иш куни мобайнида “Бажарувчи”га шартноманинг 1.1. бандига мувофиқ хизмат кўрсатиш учун зарур бўлган воситалар ва ҳужжатларни (“Бажарувчи талаб қилганда”) топширади.";
            item3.Child.Add(item3_1);

            ContractTemplateJson item3_2 = new ContractTemplateJson();
            item3_2.Title = "3.2";
            item3_2.Description = "Шартнома бўйича ишлар якунлангач, “Бажарувчи” бажарилган ишлар ҳулосаларини “Буюртмачи”га топширади.";
            item3.Child.Add(item3_2);
            jList.Add(item3);


            ContractTemplateJson item4 = new ContractTemplateJson();
            item4.Title = "4";
            item4.Description = "ТОМОНЛАРНИНГ ЖАВОБГАРЛИГИ";

            ContractTemplateJson item4_1 = new ContractTemplateJson();
            item4_1.Title = "4.1";
            item4_1.Description = "Ушбу шартнома шартларини бажармаганлиги ёки тўлиқ бажармаганлиги учун томонлар Ўзбекистон Республикасининг 1998 йил 29 августдаги 670-1-сонли “Хўжалик юритувчи субъектлар фаолиятининг шартномавий-ҳуқуқий базаси тўғрисида”ги Қонуни ва Ўзбекистон Республикаси қонунчилигининг бошқа амалдаги ҳужжатларида кўзда тутилган жавобгарликка тортилади.";
            item4.Child.Add(item4_1);
            jList.Add(item4);


            ContractTemplateJson item5 = new ContractTemplateJson();
            item5.Title = "5";
            item5.Description = "НИЗОЛАРНИ ҲАЛ ЭТИШ ТАРТИБИ";

            ContractTemplateJson item5_1 = new ContractTemplateJson();
            item5_1.Title = "5.1";
            item5_1.Description = "Тарафлар ўртасида мазкур шартнома юзасидан келиб чиқадиган низолар тарафлар келишуви билан ҳал қилинади. Келишувга эришилмаган низолар туманлараро Иқтисодий судида ҳал қилинади.";
            item5.Child.Add(item5_1);
            jList.Add(item5);


            ContractTemplateJson item6 = new ContractTemplateJson();
            item6.Title = "6";
            item6.Description = "БОШҚА ШАРТЛАР";

            ContractTemplateJson item6_1 = new ContractTemplateJson();
            item6_1.Title = "6.1";
            item6_1.Description = "Ушбу шартнома электрон рақамли имзо ёрдамида QR-код билан ёки дастурдий таъминотнинг имзо чекиш функцияси орқали ёки чоп этилган асл нусҳаларига ёзма имзо ва муҳр қўйиш йўллари билан имзоланиши мумкин.";
            item6.Child.Add(item6_1);

            ContractTemplateJson item6_2 = new ContractTemplateJson();
            item6_2.Title = "6.2";
            item6_2.Description = "Шартнома фақатгина томонларнинг келишуви асосида ўзгартирилиши мумкин. Ушбу шартномага киритиладиган ҳар қандай ўзгартириш ёки қўшимчалар расмийлаштирилиб, тарафлар томонидан имзоланади.";
            item6.Child.Add(item6_2);

            ContractTemplateJson item6_3 = new ContractTemplateJson();
            item6_3.Title = "6.3";
            item6_3.Description = "Шартномани бекор қилишга қарор қилган томон иккинчи томонга бекор қилиниши кўзланаётган муддатдан 15 кун аввал ёзма хабарнома юборади.";
            item6.Child.Add(item6_3);

            ContractTemplateJson item6_4 = new ContractTemplateJson();
            item6_4.Title = "6.4";
            item6_4.Description = "Шартнома бекор қилинганидан сўнг, шартнома юзасидан ҳақиқий харажатлар бўйича ўзаро ҳисоб-китоб қилинади.";
            item6.Child.Add(item6_4);

            ContractTemplateJson item6_5 = new ContractTemplateJson();
            item6_5.Title = "6.5";
            item6_5.Description = "Ушбу шартномада кўзда тутилмаган, қолган барча ҳолатлар юзасидан томонлар Ўзбекистон Республикасининг амалдаги қонунчилигини қўллайдилар.";
            item6.Child.Add(item6_5);
            jList.Add(item6);

            ContractTemplateJson item7 = new ContractTemplateJson();
            item7.Title = "7";
            item7.Description = "ФОРС-МАЖОР";

            ContractTemplateJson item7_1 = new ContractTemplateJson();
            item7_1.Title = "7.1";
            item7_1.Description = "Агарда енгиб бўлмас куч билан боғлиқ вазиятлар, масалан: сув тошқини, ёнғин, зилзила, эпедемия, ҳарбий можаролар, томонлар тарафидан мазкур шартнома шартларини бажаришга таъсир кўрсатувчи ҳукумат ёки маҳаллий ҳокимиятлар томонидан қабул қилинган фармойиш, буйруқ ёки бошқа маъмурий қарорлар, маъмурий ёки ҳукуматнинг чекловчи қарорлари ва ҳоказолар юзага келиш ҳоллари бевосита ёки билвосита намоён бўлган тақдирда шартнома шартларини бажариш ушбу ҳолат тугаш муддатигача узайтирилади.";
            item7.Child.Add(item7_1);

            ContractTemplateJson item7_2 = new ContractTemplateJson();
            item7_2.Title = "7.2";
            item7_2.Description = "Форс-мажор ҳолати мавжуд бўлган томон 5(беш) кун ичида бошқа томонларни форс-мажор ҳолати бошланиши ва тугаши муддатларини кўрсатган ҳолда ёзма равшда огоҳлантирилиши лозим.";
            item7.Child.Add(item7_2);
            jList.Add(item7);


            ContractTemplateJson item8 = new ContractTemplateJson();
            item8.Title = "8";
            item8.Description = "ШАРТНОМАНИНГ АМАЛ ҚИЛИШ МУДДАТИ ВА ТАРТИБИ";

            ContractTemplateJson item8_1 = new ContractTemplateJson();
            item8_1.Title = "8.1";
            item8_1.Description = "Шартнома унга томонларнинг имзо қўйган вақтдан (имзо қуйилган сана) кучга киради ҳамда ушбу шартнома бандлари тўлиқ бажарилгунга қадар амал қилади.";
            item8.Child.Add(item8_1);

            ContractTemplateJson item8_2 = new ContractTemplateJson();
            item8_2.Title = "8.2";
            item8_2.Description = "Шартнома икки нусхада - ҳар бир томон учун тузилиб, иккала томон ҳам бир ҳил юридик кучга эга.";
            item8.Child.Add(item8_2);
            jList.Add(item8);


            ContractTemplateJson item9 = new ContractTemplateJson();
            item9.Title = "9";
            item9.Description = "КОРРУПЦИЯГА ҚАРШИ КЕЛИШУВ";

            ContractTemplateJson item9_1 = new ContractTemplateJson();
            item9_1.Title = "9.1";
            item9_1.Description = "Шартнома бўйича ўз мажбуриятларинини бажаришда Томонлар коррупцияга қарши курашиш бўйича қоидаларга, шу жумладан амалдаги қонунларга риоя этилишини таъминлайди; яъни улар, уларнинг ходимлари, аффилланган шахслар, бенефициари ва шартномани амалга оширишдаги хамкорлари, пудратчилари томонидан бир-бирига ёки ташкилот ходимига пора бериш ёки пора беришда воситачилик қилишда, моддий ёки номоддий наф олишдан тийилиши лозим. Томонлар ушбу харакатларнинг олдини олиш бўйича чора-тадбирлар қабул қилинишини кафолатлайди.";
            item9.Child.Add(item9_1);

            ContractTemplateJson item9_2 = new ContractTemplateJson();
            item9_2.Title = "9.2";
            item9_2.Description = "Томонлар коррупцияга қарши қоидалар бузулганда ёки асосли гумонлар юзага келганида дархол ёзма равишда бир-бирини хабардор қилиш мажбуриятини олади. Бунда томонлар юзага келган холатга ойдинлик киритиш мақсадида ёзма изоҳ талаб қилиш хуқуқига эга ва мурожаатини олган томон 10 (ўн) иш куни мобайнида тушинтириш бериши ёки ўз фикрини билдириши мумкин.";
            item9.Child.Add(item9_2);

            ContractTemplateJson item9_3 = new ContractTemplateJson();
            item9_3.Title = "9.3";
            item9_3.Description = "Мазкур бобнинг талаблари бажарилмаганда, шу жумладан белгиланган муддатда коррупцион хавф-хатар бартараф этилмаса, томонлар амалга оширган чоралар коррупцион холатни пасайишига олиб келмаса, бошқа томон шартномани бекор қилиш хуқуқига эга ёки унинг ижросини тўхтатиб қўйиши мумкин.";
            item9.Child.Add(item9_3);

            ContractTemplateJson item9_4 = new ContractTemplateJson();
            item9_4.Title = "9.4";
            item9_4.Description = "Ушбу бобда кўрсатилган асослар бўйича шартномани бир томонлама бекор қилиш талаб қилган томонга етказилган зарарларни қоплаш қонунчиликда кўрсатилган тартибда амалга оширилади, ушбу бобнинг мажбуриятларини бузган томон эса шартноманинг бир томонлама бекор қилиниши оқибатида кўрилган зарарлар қоплашни талаб қилишига хақли эмас.";
            item9.Child.Add(item9_4);
            jList.Add(item9);

            ContractTemplateJson item10 = new ContractTemplateJson();
            item10.Title = "10";
            item10.Description = "ТОМОНЛАРНИНГ ХУҚУҚИЙ МАНЗИЛЛАРИ";
            jList.Add(item10);

            ContractTemplateJson itemButton2 = new ContractTemplateJson();
            itemButton2.IsButton = true;
            itemButton2.IsContractInfoButton = true;
            itemButton2.IsContractServiceDetailsButton = false;
            itemButton2.IsVisibleAddButton = true;
            jList.Add(itemButton2);

            return JsonConvert.SerializeObject(jList);
        }

        private string Simple()
        {
            List<ContractTemplateJson> jList = new List<ContractTemplateJson>();

            ContractTemplateJson item1 = new ContractTemplateJson();
            item1.Title = "1";
            item1.Description = "ШАРТНОМАНИНГ МАЗМУНИ ВА ИШЛАРНИНГ НАРХИ";

            ContractTemplateJson item1_1 = new ContractTemplateJson();
            item1_1.Title = "1.1";
            item1_1.Description = "“Буюртмачи” топшириғига кўра “Бажарувчи” ушбу шартномада белгиланган тартиб ва шартлар асосида қуйидаги таснифда кўрсатилган ишларни бажариш мажбуриятларини ўз зиммасига олади.";
            item1.Child.Add(item1_1);
            jList.Add(item1);

            ContractTemplateJson itemButton1 = new ContractTemplateJson();
            itemButton1.IsButton = true;
            itemButton1.IsContractInfoButton = false;
            itemButton1.IsContractServiceDetailsButton = true;
            itemButton1.IsVisibleAddButton = true;
            jList.Add(itemButton1);

            ContractTemplateJson item2 = new ContractTemplateJson();
            item2.Title = "2";
            item2.Description = "ҲИСОБ-КИТОБ ТАРТИБИ"; 

            ContractTemplateJson item2_1 = new ContractTemplateJson();
            item2_1.Title = "2.1";
            item2_1.Description = "Ушбу шартнома кучга киргач, “Буюртмачи” шартнома баҳоси тўловни “Бажарувчи”нинг ҳисоб-рақамига пул ўтказиш мажбуриятини олади.";
            item2.Child.Add(item2_1);
            jList.Add(item2);

            ContractTemplateJson item3 = new ContractTemplateJson();
            item3.Title = "3";
            item3.Description = "ИШЛАРНИ БАЖАРИШ, ТОПШИРИШ ВА ҚАБУЛ ҚИЛИШ ТАРТИБИ"; 

            ContractTemplateJson item3_1 = new ContractTemplateJson();
            item3_1.Title = "3.1";
            item3_1.Description = "Шартноманинг 2.1. банди бажарилгандан сўнг, “Буюртмачи” “Бажарувчи”га шартноманинг 1.1. бандига мувофиқ кўрсатилган хизмат ҳулосаларини “Буюртмачи”га топширади.";
            item3.Child.Add(item3_1);
            jList.Add(item3);

            ContractTemplateJson item4 = new ContractTemplateJson();
            item4.Title = "4";
            item4.Description = "БОШҚА ШАРТЛАР";

            ContractTemplateJson item4_1 = new ContractTemplateJson();
            item4_1.Title = "4.1";
            item4_1.Description = "Ушбу шартнома электрон рақамли имзо ёрдамида QR-код билан ёки дастурдий таъминотнинг имзо чекиш функцияси орқали ёки чоп этилган асл нусҳаларига ёзма имзо ва муҳр қўйиш йўллари билан имзоланиши мумкин.";
            item4.Child.Add(item4_1);
            jList.Add(item4);
             
            ContractTemplateJson item5 = new ContractTemplateJson();
            item5.Title = "5";
            item5.Description = "ШАРТНОМАНИНГ АМАЛ ҚИЛИШ МУДДАТИ ВА ТАРТИБИ";

            ContractTemplateJson item5_1 = new ContractTemplateJson();
            item5_1.Title = "5.1";
            item5_1.Description = "Шартнома унга томонларнинг имзо қўйган вақтдан (имзо қуйилган сана) кучга киради ҳамда ушбу шартнома бандлари тўлиқ бажарилгунга қадар амал қилади.";
            item5.Child.Add(item5_1);
            jList.Add(item5);

            ContractTemplateJson item6 = new ContractTemplateJson();
            item6.Title = "6";
            item6.Description = "ТОМОНЛАРНИНГ ХУҚУҚИЙ МАНЗИЛЛАРИ";
            jList.Add(item6);

            ContractTemplateJson itemButton2 = new ContractTemplateJson();
            itemButton2.IsButton = true;
            itemButton2.IsContractInfoButton = true;
            itemButton2.IsContractServiceDetailsButton = false;
            itemButton2.IsVisibleAddButton = true;
            jList.Add(itemButton2);

            return JsonConvert.SerializeObject(jList);
        }
    }
}
