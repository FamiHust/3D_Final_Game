using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public static List<Card> cardList = new List<Card>();

    void Awake()
    {
        /* Lac Viet Deck */
        //Creature cards
        cardList.Add(new Card(0, "None", 0, 0, 0,"None", Resources.Load<Sprite>("0_CardBack"), 0, 0, 0, 0, false, 0, "Common", ElementType.None));
        cardList.Add(new Card(1, "Lạc Long Quân", 2, 6, 6,"Thêm 2 lá bài lên tay", Resources.Load<Sprite>("Lac_Viet_Deck/Lac_Long_Quan"), 0, 0, 0, 0, false, 0, "Rare", ElementType.Earth));
        cardList.Add(new Card(2, "Cờ Binh Lạc Việt", 3, 3, 3," ", Resources.Load<Sprite>("Lac_Viet_Deck/Co_binh"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(3, "Cung thủ Lạc Việt", 2, 1, 1," ", Resources.Load<Sprite>("Lac_Viet_Deck/Cung_thu"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(4, "Dũng tướng tiên phong", 4, 3, 3," ", Resources.Load<Sprite>("Lac_Viet_Deck/Dung_tuong"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(5, "Giáp sắt", 4, 4, 4," ", Resources.Load<Sprite>("Lac_Viet_Deck/Giap_sat"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(6, "Khiên thủ Lạc Việt", 2, 3, 3," ", Resources.Load<Sprite>("Lac_Viet_Deck/Khien_thu"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(7, "Kỵ binh xông xáo", 2, 3, 1," ", Resources.Load<Sprite>("Lac_Viet_Deck/Ky_binh"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(8, "Lính tiên phong", 1, 1, 1," ", Resources.Load<Sprite>("Lac_Viet_Deck/Linh_tien_phong"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(9, "Lạc tướng khôn ngoan", 3, 3, 3," ", Resources.Load<Sprite>("Lac_Viet_Deck/Lac_tuong"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(10, "Lương Khan-Chiến thần Lạc Việt", 3, 1, 3," ", Resources.Load<Sprite>("Lac_Viet_Deck/Luong_Khan"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(11, "Lạc Hầu tận tụy", 3, 1, 3," ", Resources.Load<Sprite>("Lac_Viet_Deck/Lac_Hau"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(12, "Mũ sắt", 2, 3, 3," ", Resources.Load<Sprite>("Lac_Viet_Deck/Mu_sat"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(13, "Ngựa sắt", 5, 5, 5," ", Resources.Load<Sprite>("Lac_Viet_Deck/Ngua_sat"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(14, "Roi sắt", 3, 4, 4," ", Resources.Load<Sprite>("Lac_Viet_Deck/Roi_sat"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(15, "Sứ giả Lạc Việt", 2, 3, 3," ", Resources.Load<Sprite>("Lac_Viet_Deck/Su_gia"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(16, "Thần rèn Lạc Việt", 3, 4, 4," ", Resources.Load<Sprite>("Lac_Viet_Deck/Than_ren"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(17, "Thợ rèn Lạc Việt", 2, 1, 2," ", Resources.Load<Sprite>("Lac_Viet_Deck/Tho_ren"),0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(18, "Kẻ cuồng Long tộc", 2, 2, 2," ", Resources.Load<Sprite>("Lac_Viet_Deck/Ke_cuong_Long_toc"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(19, "Cồng binh Lạc Việt", 1, 2, 2," ", Resources.Load<Sprite>("Lac_Viet_Deck/Cong_binh"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(20, "Sơ cứu nhiệt tình", 2, 1, 2," ", Resources.Load<Sprite>("Lac_Viet_Deck/So_cuu_nhiet_tinh"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(21, "Gióng", 2, 1, 5," ", Resources.Load<Sprite>("Lac_Viet_Deck/Giong"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));

        //Spell Cards
        cardList.Add(new Card(22, "Chiến nỏ", 1, 0, 0,"-1 HP của kẻ địch", Resources.Load<Sprite>("Lac_Viet_Deck/Chien_no"), 0, 0, 0, 0, true, 1, "Common", ElementType.Earth));
        cardList.Add(new Card(23, "Tĩnh dưỡng", 1, 0, 0,"+2 HP, +2 Mana", Resources.Load<Sprite>("Lac_Viet_Deck/Tinh_duong"), 0, 2, 0, 2, true, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(24, "Hố chông", 1, 0, 0,"-2 HP của kẻ địch", Resources.Load<Sprite>("Lac_Viet_Deck/Ho_chong"), 0, 0, 0, 0, true, 2, "Common", ElementType.Earth));
        cardList.Add(new Card(25, "Công thành bất ngờ", 1, 0, 0,"-3 HP của kẻ địch", Resources.Load<Sprite>("Lac_Viet_Deck/Cong_thanh"), 0, 0, 0, 0, true, 3, "Common", ElementType.Earth));
        cardList.Add(new Card(26, "Liên hoàn sấm sét", 1, 0, 0,"-4 HP của kẻ địch", Resources.Load<Sprite>("Lac_Viet_Deck/Sam_set"), 0, 0, 0, 0, true, 4, "Common", ElementType.Earth));
        cardList.Add(new Card(27, "Bất hoại", 1, 0, 0,"+3 HP", Resources.Load<Sprite>("Lac_Viet_Deck/Bat_hoai"), 0, 0, 0, 3, true, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(28, "Trao kiếm", 1, 0, 0," ", Resources.Load<Sprite>("Lac_Viet_Deck/Trao_kiem"), 2, 0, 0, 0, true, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(29, "Chiến trường không khoan nhượng", 1, 0, 0," ", Resources.Load<Sprite>("Lac_Viet_Deck/Chien_truong"), 0, 0, 0, 0, true, 5, "Common", ElementType.Earth));
        cardList.Add(new Card(30, "Đồng lòng chiến đấu", 1, 0, 0," ", Resources.Load<Sprite>("Lac_Viet_Deck/Dong_long_chien_dau"), 0, 0, 1, 0, true, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(31, "Bắt cóc", 1, 0, 0," ", Resources.Load<Sprite>("Lac_Viet_Deck/Bat_coc"), 0, 0, 1, 0, true, 0, "Common", ElementType.Earth));

        /* Thủy Tinh Deck*/
        // Creature Cards
        cardList.Add(new Card(32, "Thủy Tinh", 2, 1, 6," ", Resources.Load<Sprite>("Thuy_Tinh_Deck/Thuy_Tinh"), 0, 0, 0, 0, false, 0, "Rare", ElementType.Water));
        cardList.Add(new Card(33, "Binh tôm", 1, 1, 1," ", Resources.Load<Sprite>("Thuy_Tinh_Deck/Binh_tom"), 0, 0, 0, 0, false, 0, "Common", ElementType.Water));
        cardList.Add(new Card(34, "Bạch tuộc 6 vòi", 4, 2, 4," ", Resources.Load<Sprite>("Thuy_Tinh_Deck/Bach_tuoc"), 0, 0, 0, 0, false, 0, "Common", ElementType.Water));
        cardList.Add(new Card(35, "Bạch tuộc 9 vòi", 6, 4, 5," ", Resources.Load<Sprite>("Thuy_Tinh_Deck/Bach_tuoc_9_voi"), 0, 0, 0, 0, false, 0, "Common", ElementType.Water));
        cardList.Add(new Card(36, "Cao xạ pháo ngư", 2, 2, 1," ", Resources.Load<Sprite>("Thuy_Tinh_Deck/Phao_ngu"), 0, 0, 0, 0, false, 0, "Common", ElementType.Water));
        cardList.Add(new Card(37, "Chình điện", 2, 2, 1," ", Resources.Load<Sprite>("Thuy_Tinh_Deck/Chinh_dien"), 0, 0, 0, 0, false, 0, "Common", ElementType.Water));
        cardList.Add(new Card(38, "Cá mất trí", 2, 1, 1," ", Resources.Load<Sprite>("Thuy_Tinh_Deck/Ca_mat_tri"), 0, 0, 0, 0, false, 0, "Common", ElementType.Water));
        cardList.Add(new Card(39, "Cá mập đầu búa", 3, 3, 3," ", Resources.Load<Sprite>("Thuy_Tinh_Deck/Ca_map"), 0, 0, 0, 0, false, 0, "Common", ElementType.Water));
        cardList.Add(new Card(40, "Ghẹ tướng gai góc", 4, 1, 5," ", Resources.Load<Sprite>("Thuy_Tinh_Deck/Ghe_tuong"), 0, 0, 0, 0, false, 0, "Common", ElementType.Water));
        cardList.Add(new Card(41, "Long tướng phẫn uất", 3, 2, 2," ", Resources.Load<Sprite>("Thuy_Tinh_Deck/Long_tuong"), 0, 0, 0, 0, false, 0, "Common", ElementType.Water));
        cardList.Add(new Card(42, "Lính chình giấu nghề", 2, 1, 3," ", Resources.Load<Sprite>("Thuy_Tinh_Deck/Linh_chinh"), 0, 0, 0, 0, false, 0, "Common", ElementType.Water));
        cardList.Add(new Card(43, "Lính cá", 1, 2, 1," ", Resources.Load<Sprite>("Thuy_Tinh_Deck/Linh_ca"), 0, 0, 0, 0, false, 0, "Common", ElementType.Water));
        cardList.Add(new Card(44, "Lính lác Thủy tộc", 2, 2, 1," ", Resources.Load<Sprite>("Thuy_Tinh_Deck/Linh_lac"), 0, 0, 0, 0, false, 0, "Common", ElementType.Water));
        cardList.Add(new Card(45, "Ma sứa điện", 1, 1, 1," ", Resources.Load<Sprite>("Thuy_Tinh_Deck/Ma_sua"), 0, 0, 0, 0, false, 0, "Common", ElementType.Water));
        cardList.Add(new Card(46, "Sùng Lang", 6, 3, 5,"Trấn ải Long Quân", Resources.Load<Sprite>("Thuy_Tinh_Deck/Sung_Lang"), 0, 0, 0, 0, false, 0, "Common", ElementType.Water));
        cardList.Add(new Card(47, "Sùng Lộc", 5, 1, 4,"Tể tướng của Thủy tộc", Resources.Load<Sprite>("Thuy_Tinh_Deck/Sung_Loc"), 0, 0, 0, 0, false, 0, "Common", ElementType.Water));
        cardList.Add(new Card(48, "Ếch binh", 3, 3, 2," ", Resources.Load<Sprite>("Thuy_Tinh_Deck/Ech_binh"), 0, 0, 0, 0, false, 0, "Common", ElementType.Water));
        cardList.Add(new Card(49, "Sùng Lộc", 1, 1, 5," ", Resources.Load<Sprite>("Thuy_Tinh_Deck/Sung_Loc_2"), 0, 0, 0, 0, false, 0, "Common", ElementType.Water));
        cardList.Add(new Card(50, "Sấu tướng tha hóa", 3, 3, 1," ", Resources.Load<Sprite>("Thuy_Tinh_Deck/Sau_tuong"), 0, 0, 0, 0, false, 0, "Common", ElementType.Water));
        cardList.Add(new Card(51, "Lily, Thủy nương quyến rũ", 4, 1, 4," ", Resources.Load<Sprite>("Thuy_Tinh_Deck/Lily"), 0, 0, 0, 0, false, 0, "Common", ElementType.Water));
        
        /* Sơn Tinh Deck*/
        cardList.Add(new Card(52, "Sơn Tinh", 2, 1, 7," ", Resources.Load<Sprite>("Son_Tinh_Deck/Son_Tinh"), 0, 0, 0, 0, false, 0, "Rare", ElementType.Earth));
        cardList.Add(new Card(53, "Beo hoa sát thủ", 3, 2, 2," ", Resources.Load<Sprite>("Son_Tinh_Deck/Beo_hoa"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(54, "Cu li Sơn tộc", 3, 3, 3," ", Resources.Load<Sprite>("Son_Tinh_Deck/Cu_li"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(55, "Gà 6 cựa", 5, 3, 5," ", Resources.Load<Sprite>("Son_Tinh_Deck/Ga_6_cua"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(56, "Gà 9 cựa", 6, 4, 6," ", Resources.Load<Sprite>("Son_Tinh_Deck/Ga_9_cua"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(57, "Gấu con hăng hái", 2, 2, 2," ", Resources.Load<Sprite>("Son_Tinh_Deck/Gau_con"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(58, "Hùng tướng say máu", 3, 1, 3," ", Resources.Load<Sprite>("Son_Tinh_Deck/Hung_tuong"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(59, "Học sĩ chim cu gáy", 3, 1, 3," ", Resources.Load<Sprite>("Son_Tinh_Deck/Chim_cu_gay"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(60, "Lang băm hươu sao", 2, 2, 2," ", Resources.Load<Sprite>("Son_Tinh_Deck/Huou_sao"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(61, "Lính sói", 1, 1, 2," ", Resources.Load<Sprite>("Son_Tinh_Deck/Linh_soi"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(62, "Lục Lục", 3, 2, 2," ", Resources.Load<Sprite>("Son_Tinh_Deck/Luc_Luc"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(63, "Ngựa 6 hồng mao", 4, 3, 3," ", Resources.Load<Sprite>("Son_Tinh_Deck/Ngua_6_mao"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(64, "Ngựa 9 hồng mao", 5, 3, 5," ", Resources.Load<Sprite>("Son_Tinh_Deck/Ngua_9_mao"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(65, "Nhím nhặt nhạnh", 2, 3, 3," ", Resources.Load<Sprite>("Son_Tinh_Deck/Nhim_nhat_nhanh"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(66, "Sói đàn", 2, 1, 2," ", Resources.Load<Sprite>("Son_Tinh_Deck/Soi_dan"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(67, "Tai Đỏ", 3, 2, 3," ", Resources.Load<Sprite>("Son_Tinh_Deck/Tai_Do"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(68, "Tay Đen", 5, 3, 5," ", Resources.Load<Sprite>("Son_Tinh_Deck/Tay_Den"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(69, "Tham Lang", 3, 1, 5," ", Resources.Load<Sprite>("Son_Tinh_Deck/Tham_Lang"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(70, "Thầy lang voọc", 1, 1, 1," ", Resources.Load<Sprite>("Son_Tinh_Deck/Thay_lang_vooc"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(71, "Thỏ bầy", 1, 1, 1," ", Resources.Load<Sprite>("Son_Tinh_Deck/Tho_bay"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(72, "Trâu tướng say máu", 4, 1, 5," ", Resources.Load<Sprite>("Son_Tinh_Deck/Trau_tuong"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(73, "Tê giác binh", 4, 4, 2," ", Resources.Load<Sprite>("Son_Tinh_Deck/Te_giac_binh"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(74, "Tê giác phá hoại", 4, 1, 4," ", Resources.Load<Sprite>("Son_Tinh_Deck/Te_giac_pha_hoai"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(75, "Tí Nị", 4, 2, 4," ", Resources.Load<Sprite>("Son_Tinh_Deck/Ti_Ni"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(76, "Voi 6 ngà", 6, 6, 6," ", Resources.Load<Sprite>("Son_Tinh_Deck/Voi_6_nga"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(77, "Voi 9 ngà", 6, 6, 6," ", Resources.Load<Sprite>("Son_Tinh_Deck/Voi_9_nga"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(78, "Voi đá", 2, 4, 4," ", Resources.Load<Sprite>("Son_Tinh_Deck/Voi_da"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(79, "Đầu bếp dã trư", 2, 2, 2," ", Resources.Load<Sprite>("Son_Tinh_Deck/Da_tru"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(80, "Đại hùng Sơn tướng", 3, 2, 4," ", Resources.Load<Sprite>("Son_Tinh_Deck/Dai_hung"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(81, "Hổ Ca", 2, 1, 6," ", Resources.Load<Sprite>("Son_Tinh_Deck/Ho_ca"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(82, "Tí Nị", 2, 1, 6," ", Resources.Load<Sprite>("Son_Tinh_Deck/Ti_Ni_2"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(83, "Do thám liều lĩnh", 3, 2, 2," ", Resources.Load<Sprite>("Son_Tinh_Deck/Do_tham"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(84, "Voọc chờ thời", 3, 1, 3," ", Resources.Load<Sprite>("Son_Tinh_Deck/Vooc_cho_thoi"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(85, "Lang tộc thám thính", 2, 2, 2," ", Resources.Load<Sprite>("Son_Tinh_Deck/Lang_toc_tham_thinh"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(86, "Chó rừng tò mò", 2, 2, 2," ", Resources.Load<Sprite>("Son_Tinh_Deck/Cho_rung"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(87, "Tham Lang, kẻ bị lật đổ", 2, 2, 1," ", Resources.Load<Sprite>("Son_Tinh_Deck/Tham_lang_2"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(88, "Thuận Thiên Hổ", 3, 3, 3," ", Resources.Load<Sprite>("Son_Tinh_Deck/Thuan_thien_ho"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(89, "Lang tộc dẫn đầu", 2, 3, 3," ", Resources.Load<Sprite>("Son_Tinh_Deck/Lang_toc_dan_dau"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(90, "Lang tộc viện binh", 4, 4, 4," ", Resources.Load<Sprite>("Son_Tinh_Deck/Lang_toc_vien_binh"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        
    }
}
