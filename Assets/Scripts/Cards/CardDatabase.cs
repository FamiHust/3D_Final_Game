using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public static List<Card> cardList = new List<Card>();

    void Awake()
    {
        /* Lac Viet Deck */
        cardList.Add(new Card(0, "None", 0, 0, 0, "None", Resources.Load<Sprite>("0_CardBack"), 0, 0, 0, 0, false, 0, "Common", ElementType.None));
        cardList.Add(new Card(1, "Lạc Long Quân", 2, 4, 6, "[Đại dương]", Resources.Load<Sprite>("Lac_Viet_Deck/Lac_Long_Quan"), 0, 0, 0, 0, false, 0, "Rare", ElementType.Water));
        cardList.Add(new Card(2, "Cờ Binh Lạc Việt", 1, 1, 3, "[Thảo nguyên]", Resources.Load<Sprite>("Lac_Viet_Deck/Co_binh"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(3, "Cung thủ Lạc Việt", 2, 1, 1, "[Thảo nguyên]", Resources.Load<Sprite>("Lac_Viet_Deck/Cung_thu"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(4, "Dũng tướng tiên phong", 4, 3, 3, "[Thảo nguyên]", Resources.Load<Sprite>("Lac_Viet_Deck/Dung_tuong"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(5, "Giáp sắt", 4, 2, 2, "[Thảo nguyên]", Resources.Load<Sprite>("Lac_Viet_Deck/Giap_sat"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(6, "Khiên thủ Lạc Việt", 2, 2, 3, "[Thảo nguyên]", Resources.Load<Sprite>("Lac_Viet_Deck/Khien_thu"), 0, 0, 0, 0, false, 0, "Rare", ElementType.Earth));
        cardList.Add(new Card(7, "Kỵ binh xông xáo", 2, 3, 1, "[Thảo nguyên]", Resources.Load<Sprite>("Lac_Viet_Deck/Ky_binh"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(8, "Lính tiên phong", 1, 1, 1, "[Thảo nguyên]", Resources.Load<Sprite>("Lac_Viet_Deck/Linh_tien_phong"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(9, "Lạc tướng khôn ngoan", 3, 3, 3, "[Thảo nguyên]", Resources.Load<Sprite>("Lac_Viet_Deck/Lac_tuong"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(10, "Lương Khan-Chiến thần Lạc Việt", 3, 4, 3, "[Thảo nguyên][Rút thêm 2 lá từ bộ bài]", Resources.Load<Sprite>("Lac_Viet_Deck/Luong_Khan"), 2, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(11, "Lạc Hầu tận tụy", 3, 1, 3, "[Thảo nguyên]", Resources.Load<Sprite>("Lac_Viet_Deck/Lac_Hau"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(12, "Mũ sắt", 2, 3, 3, "[Thảo nguyên]", Resources.Load<Sprite>("Lac_Viet_Deck/Mu_sat"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(13, "Ngựa sắt", 5, 5, 5, "[Thảo nguyên]", Resources.Load<Sprite>("Lac_Viet_Deck/Ngua_sat"), 0, 0, 0, 0, false, 0, "Rare", ElementType.Earth));
        cardList.Add(new Card(14, "Roi sắt", 4, 4, 4, "[Thảo nguyên]", Resources.Load<Sprite>("Lac_Viet_Deck/Roi_sat"), 0, 0, 0, 0, false, 0, "Rare", ElementType.Earth));
        cardList.Add(new Card(15, "Sứ giả Lạc Việt", 3, 3, 3, "[Thảo nguyên]", Resources.Load<Sprite>("Lac_Viet_Deck/Su_gia"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(16, "Thần rèn Lạc Việt", 3, 0, 4, "[Thảo nguyên]", Resources.Load<Sprite>("Lac_Viet_Deck/Than_ren"), 0, 0, 0, 0, false, 0, "Rare", ElementType.Earth));
        cardList.Add(new Card(17, "Thợ rèn Lạc Việt", 2, 1, 2, "[Thảo nguyên]", Resources.Load<Sprite>("Lac_Viet_Deck/Tho_ren"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(18, "Kẻ cuồng Long tộc", 2, 0, 2, "[Thảo nguyên]", Resources.Load<Sprite>("Lac_Viet_Deck/Ke_cuong_Long_toc"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(19, "Cồng binh Lạc Việt", 1, 0, 2, "[Thảo nguyên]", Resources.Load<Sprite>("Lac_Viet_Deck/Cong_binh"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(20, "Sơ cứu nhiệt tình", 0, 1, 2, "[Thảo nguyên] +1 Hp cho người chơi", Resources.Load<Sprite>("Lac_Viet_Deck/So_cuu_nhiet_tinh"), 0, 0, 0, 1, false, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(21, "Gióng", 2, 1, 5, "[Thảo nguyên]", Resources.Load<Sprite>("Lac_Viet_Deck/Giong"), 0, 0, 0, 0, false, 0, "Common", ElementType.Earth));

        cardList.Add(new Card(22, "Chiến nỏ", 1, 0, 0, "[Thảo nguyên x Phép] -1 HP của kẻ địch", Resources.Load<Sprite>("Lac_Viet_Deck/Chien_no"), 0, 0, 0, 0, true, 1, "Common", ElementType.Earth));
        cardList.Add(new Card(23, "Tĩnh dưỡng", 1, 0, 0, "[Thảo nguyên x Phép] +2 HP cho người chơi", Resources.Load<Sprite>("Lac_Viet_Deck/Tinh_duong"), 0, 0, 0, 2, true, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(24, "Hố chông", 2, 0, 0, "[Thảo nguyên x Phép] -2 HP của kẻ địch", Resources.Load<Sprite>("Lac_Viet_Deck/Ho_chong"), 0, 0, 0, -2, true, 2, "Common", ElementType.Earth));
        cardList.Add(new Card(25, "Công thành bất ngờ", 3, 0, 0, "[Thảo nguyên x Phép] -3 HP của kẻ địch", Resources.Load<Sprite>("Lac_Viet_Deck/Cong_thanh"), 0, 0, 0, 0, true, 3, "Common", ElementType.Earth));
        cardList.Add(new Card(26, "Liên hoàn sấm sét", 2, 0, 0, "[Thảo nguyên x Phép] -4 HP của kẻ địch", Resources.Load<Sprite>("Lac_Viet_Deck/Sam_set"), 0, 0, 0, 0, true, 4, "Common", ElementType.Earth));
        cardList.Add(new Card(27, "Kim cương bất hoại", 3, 0, 0, "[Thảo nguyên x Phép] +3 HP cho người chơi", Resources.Load<Sprite>("Lac_Viet_Deck/Bat_hoai"), 0, 0, 0, 3, true, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(28, "Trao kiếm", 2, 0, 0, "[Thảo nguyên x Phép] Rút thêm 2 lá từ bộ bài", Resources.Load<Sprite>("Lac_Viet_Deck/Trao_kiem"), 2, 0, 0, 0, true, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(29, "Chiến trường không khoan nhượng", 2, 0, 0, "[Thảo nguyên x Phép] -5 HP của cả quân ta lần quân địch", Resources.Load<Sprite>("Lac_Viet_Deck/Chien_truong"), 0, 0, 0, -5, true, 5, "Common", ElementType.Earth));
        cardList.Add(new Card(30, "Đồng lòng chiến đấu", 4, 0, 0, "[Thảo nguyên x Phép] Hồi sinh 1 lá dưới mộ bài", Resources.Load<Sprite>("Lac_Viet_Deck/Dong_long_chien_dau"), 0, 0, 1, 0, true, 0, "Common", ElementType.Earth));
        cardList.Add(new Card(31, "Bắt cóc", 2, 0, 0, "[Thảo nguyên x Phép] Rút thêm 3 lá từ bộ bài", Resources.Load<Sprite>("Lac_Viet_Deck/Bat_coc"), 3, 0, 0, 0, true, 0, "Common", ElementType.Earth));

        /* Thủy Tinh Deck*/
        cardList.Add(new Card(32, "Thủy Tinh", 4, 4, 6, "[Đại dương]", Resources.Load<Sprite>("Thuy_Tinh_Deck/Thuy_Tinh"), 0, 0, 0, 0, false, 0, "Rare", ElementType.Water));
        cardList.Add(new Card(33, "Binh tôm", 1, 2, 3, "[Đại dương]", Resources.Load<Sprite>("Thuy_Tinh_Deck/Binh_tom"), 0, 0, 0, 0, false, 0, "Common", ElementType.Water));
        cardList.Add(new Card(34, "Bạch tuộc 6 vòi", 4, 2, 4, "[Đại dương]", Resources.Load<Sprite>("Thuy_Tinh_Deck/Bach_tuoc"), 0, 0, 0, 0, false, 0, "Common", ElementType.Water));
        cardList.Add(new Card(35, "Bạch tuộc 9 vòi", 6, 4, 5, "[Đại dương]", Resources.Load<Sprite>("Thuy_Tinh_Deck/Bach_tuoc_9_voi"), 0, 0, 0, 0, false, 0, "Rare", ElementType.Water));
        cardList.Add(new Card(36, "Cá nóc độc", 0, 0, 3, "[Đại dương]", Resources.Load<Sprite>("Thuy_Tinh_Deck/Ca_noc_doc"), 0, 0, 0, 0, false, 0, "Common", ElementType.Water));
        cardList.Add(new Card(37, "Chình điện", 2, 2, 1, "[Đại dương]", Resources.Load<Sprite>("Thuy_Tinh_Deck/Chinh_dien"), 0, 0, 0, 0, false, 0, "Common", ElementType.Water));
        cardList.Add(new Card(38, "Cá mất trí", 2, 3, 3, "[Đại dương][Bốc thêm 2 lá]", Resources.Load<Sprite>("Thuy_Tinh_Deck/Ca_mat_tri"), 2, 0, 0, 0, false, 0, "Common", ElementType.Water));
        cardList.Add(new Card(39, "Cá mập đầu búa", 3, 2, 4, "[Đại dương]", Resources.Load<Sprite>("Thuy_Tinh_Deck/Ca_map"), 0, 0, 0, 0, false, 0, "Common", ElementType.Water));
        cardList.Add(new Card(40, "Ghẹ tướng gai góc", 4, 0, 5, "[Đại dương]", Resources.Load<Sprite>("Thuy_Tinh_Deck/Ghe_tuong"), 0, 0, 0, 0, false, 0, "Common", ElementType.Water));
        cardList.Add(new Card(41, "Long tướng phẫn uất", 3, 3, 2, "[Đại dương]", Resources.Load<Sprite>("Thuy_Tinh_Deck/Long_tuong"), 0, 0, 0, 0, false, 0, "Common", ElementType.Water));
        cardList.Add(new Card(42, "Lính chình giấu nghề", 2, 1, 3, "[Đại dương]", Resources.Load<Sprite>("Thuy_Tinh_Deck/Linh_chinh"), 0, 0, 0, 0, false, 0, "Common", ElementType.Water));
        cardList.Add(new Card(43, "Lính cá", 1, 2, 1, "[Đại dương]", Resources.Load<Sprite>("Thuy_Tinh_Deck/Linh_ca"), 0, 0, 0, 0, false, 0, "Common", ElementType.Water));
        cardList.Add(new Card(44, "Lính lác Thủy tộc", 2, 2, 1, "[Đại dương]", Resources.Load<Sprite>("Thuy_Tinh_Deck/Linh_lac"), 0, 0, 0, 0, false, 0, "Common", ElementType.Water));
        cardList.Add(new Card(45, "Ma sứa điện", 1, 1, 1, "[Đại dương]", Resources.Load<Sprite>("Thuy_Tinh_Deck/Ma_sua"), 0, 0, 0, 0, false, 0, "Common", ElementType.Water));
        cardList.Add(new Card(46, "Sùng Lang, trấn ải Long Quân", 6, 3, 5, "[Đại dương] Sừng sững như một hòn đảo trồi lên giữa biển sâu", Resources.Load<Sprite>("Thuy_Tinh_Deck/Sung_Lang"), 0, 0, 0, 0, false, 0, "Rare", ElementType.Water));
        cardList.Add(new Card(47, "Sùng Lộc, tể tướng Thủy tộc", 5, 1, 4, "[Đại dương] Là chú và thầy của Thủy tinh-Thách Long Quân. Kiêu hãnh, lạnh lùng và cực đoan", Resources.Load<Sprite>("Thuy_Tinh_Deck/Sung_Loc"), 0, 0, 0, 0, false, 0, "Rare", ElementType.Water));
        cardList.Add(new Card(48, "Ếch binh", 3, 3, 2, "[Đại dương]", Resources.Load<Sprite>("Thuy_Tinh_Deck/Ech_binh"), 0, 0, 0, 0, false, 0, "Common", ElementType.Water));
        cardList.Add(new Card(49, "Sùng Lộc", 1, 1, 5, "[Đại dương] Tể tướng của Thủy tộc", Resources.Load<Sprite>("Thuy_Tinh_Deck/Sung_Loc_2"), 0, 0, 0, 0, false, 0, "Rare", ElementType.Water));
        cardList.Add(new Card(50, "Sấu tướng tha hóa", 3, 3, 1, "[Đại dương]", Resources.Load<Sprite>("Thuy_Tinh_Deck/Sau_tuong"), 0, 0, 0, 0, false, 0, "Common", ElementType.Water));
        cardList.Add(new Card(51, "Lily, Thủy nương quyến rũ", 4, 1, 4, "[Đại dương]", Resources.Load<Sprite>("Thuy_Tinh_Deck/Lily"), 0, 0, 0, 0, false, 0, "Rare", ElementType.Water));
        cardList.Add(new Card(52, "Chình sát thủ", 2, 3, 1, "[Đại dương]", Resources.Load<Sprite>("Thuy_Tinh_Deck/Chinh_sat_thu"), 0, 0, 0, 0, false, 0, "Common", ElementType.Water));
        cardList.Add(new Card(53, "Cá mập tay búa", 3, 2, 4, "[Đại dương]", Resources.Load<Sprite>("Thuy_Tinh_Deck/Tay_bua"), 0, 0, 0, 0, false, 0, "Common", ElementType.Water));

        /* Sơn Tinh Deck*/
        cardList.Add(new Card(54, "Sơn Tinh", 2, 5, 7, "[Rừng núi]", Resources.Load<Sprite>("Son_Tinh_Deck/Son_Tinh"), 0, 0, 0, 0, false, 0, "Rare", ElementType.Forest));
        cardList.Add(new Card(55, "Beo hoa sát thủ", 3, 2, 2, "[Rừng núi]", Resources.Load<Sprite>("Son_Tinh_Deck/Beo_hoa"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(56, "Cu li Sơn tộc", 3, 0, 3, "[Rừng núi]", Resources.Load<Sprite>("Son_Tinh_Deck/Cu_li"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(57, "Gà 6 cựa", 5, 3, 5, "[Rừng núi]", Resources.Load<Sprite>("Son_Tinh_Deck/Ga_6_cua"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(58, "Gà 9 cựa", 6, 4, 6, "[Rừng núi]", Resources.Load<Sprite>("Son_Tinh_Deck/Ga_9_cua"), 0, 0, 0, 0, false, 0, "Rare", ElementType.Forest));
        cardList.Add(new Card(59, "Gấu con hăng hái", 2, 2, 2, "[Rừng núi]", Resources.Load<Sprite>("Son_Tinh_Deck/Gau_con"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(60, "Hùng tướng say máu", 3, 1, 3, "[Rừng núi]", Resources.Load<Sprite>("Son_Tinh_Deck/Hung_tuong"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(61, "Tiểu tích", 2, 1, 1, "[Rừng núi]", Resources.Load<Sprite>("Son_Tinh_Deck/Tieu_tich"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(62, "Hổ binh hiếu chiến", 3, 2, 4, "[Rừng núi]", Resources.Load<Sprite>("Son_Tinh_Deck/Ho_binh"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(63, "Lính sói", 1, 1, 2, "[Rừng núi]", Resources.Load<Sprite>("Son_Tinh_Deck/Linh_soi"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(64, "Nhím nhỏ cứng đầu", 1, 1, 4, "[Rừng núi]", Resources.Load<Sprite>("Son_Tinh_Deck/Nhim_nho"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(65, "Ngựa 6 hồng mao", 4, 3, 3, "[Rừng núi]", Resources.Load<Sprite>("Son_Tinh_Deck/Ngua_6_mao"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(66, "Ngựa 9 hồng mao", 5, 3, 5, "[Rừng núi]", Resources.Load<Sprite>("Son_Tinh_Deck/Ngua_9_mao"), 0, 0, 0, 0, false, 0, "Rare", ElementType.Forest));
        cardList.Add(new Card(67, "Chó rừng tò mò", 2, 2, 2, "[Rừng núi]", Resources.Load<Sprite>("Son_Tinh_Deck/Cho_rung"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(68, "Sói đàn", 2, 1, 2, "[Rừng núi]", Resources.Load<Sprite>("Son_Tinh_Deck/Soi_dan"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(69, "Tai Đỏ", 2, 2, 3, "[Rừng núi]", Resources.Load<Sprite>("Son_Tinh_Deck/Tai_Do"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(70, "Tay Đen", 5, 3, 5, "[Rừng núi]", Resources.Load<Sprite>("Son_Tinh_Deck/Tay_Den"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(71, "Trâu gầy điên tiết", 3, 1, 4, "[Rừng núi]", Resources.Load<Sprite>("Son_Tinh_Deck/Trau_gay"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(72, "Song dao sao la", 3, 3, 4, "[Rừng núi]", Resources.Load<Sprite>("Son_Tinh_Deck/Song_dao"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(73, "Sao la giấu nghề", 4, 1, 4, "[Rừng núi]", Resources.Load<Sprite>("Son_Tinh_Deck/Sao_la"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(74, "Trâu tướng say máu", 4, 1, 5, "[Rừng núi]", Resources.Load<Sprite>("Son_Tinh_Deck/Trau_tuong"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(75, "Tê giác binh", 4, 4, 2, "[Rừng núi]", Resources.Load<Sprite>("Son_Tinh_Deck/Te_giac_binh"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(76, "Tê giác phá hoại", 4, 1, 4, "[Rừng núi]", Resources.Load<Sprite>("Son_Tinh_Deck/Te_giac_pha_hoai"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(77, "Tí Nị", 4, 2, 4, "[Rừng núi]", Resources.Load<Sprite>("Son_Tinh_Deck/Ti_Ni"), 0, 0, 0, 0, false, 0, "Rare", ElementType.Forest));
        cardList.Add(new Card(78, "Voi 6 ngà", 6, 6, 6, "[Rừng núi]", Resources.Load<Sprite>("Son_Tinh_Deck/Voi_6_nga"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(79, "Voi 9 ngà", 6, 6, 6, "[Rừng núi]", Resources.Load<Sprite>("Son_Tinh_Deck/Voi_9_nga"), 0, 0, 0, 0, false, 0, "Rare", ElementType.Forest));
        cardList.Add(new Card(80, "Voi đá", 0, 0, 4, "[Rừng núi]", Resources.Load<Sprite>("Son_Tinh_Deck/Voi_da"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(81, "Đầu bếp dã trư", 2, 0, 2, "[Rừng núi]", Resources.Load<Sprite>("Son_Tinh_Deck/Da_tru"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(82, "Đại hùng Sơn tướng", 3, 2, 4, "[Rừng núi]", Resources.Load<Sprite>("Son_Tinh_Deck/Dai_hung"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(83, "Hổ Ca", 2, 1, 6, "[Rừng núi]", Resources.Load<Sprite>("Son_Tinh_Deck/Ho_ca"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(84, "Tí Nị", 2, 1, 6, "[Rừng núi]", Resources.Load<Sprite>("Son_Tinh_Deck/Ti_Ni_2"), 0, 0, 0, 0, false, 0, "Rare", ElementType.Forest));
        cardList.Add(new Card(85, "Do thám liều lĩnh", 3, 2, 2, "[Rừng núi]", Resources.Load<Sprite>("Son_Tinh_Deck/Do_tham"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(86, "Voọc chờ thời", 3, 0, 3, "[Rừng núi]", Resources.Load<Sprite>("Son_Tinh_Deck/Vooc_cho_thoi"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(87, "Lang tộc thám thính", 2, 1, 2, "[Rừng núi]", Resources.Load<Sprite>("Son_Tinh_Deck/Lang_toc_tham_thinh"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(88, "Chó rừng tò mò", 2, 2, 2, "[Rừng núi]", Resources.Load<Sprite>("Son_Tinh_Deck/Cho_rung"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(89, "Tham Lang, kẻ bị lật đổ", 2, 2, 1, "[Rừng núi]", Resources.Load<Sprite>("Son_Tinh_Deck/Tham_lang_2"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(90, "Thuận Thiên Hổ", 3, 3, 3, "[Rừng núi]", Resources.Load<Sprite>("Son_Tinh_Deck/Thuan_thien_ho"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(91, "Lang tộc dẫn đầu", 2, 2, 3, "[Rừng núi]", Resources.Load<Sprite>("Son_Tinh_Deck/Lang_toc_dan_dau"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(92, "Lang tộc viện binh", 4, 4, 4, "[Rừng núi] +2 Hp cho người chơi", Resources.Load<Sprite>("Son_Tinh_Deck/Lang_toc_vien_binh"), 0, 0, 0, 2, false, 0, "Common", ElementType.Forest));

        /*Yêu Ma Deck*/
        cardList.Add(new Card(93, "Mũ Trùm", 1, 1, 2, "[Đầm lầy]", Resources.Load<Sprite>("Yeu_Ma_Deck/Mu_trum"), 0, 0, 0, 0, false, 0, "Rare", ElementType.Swamp));
        cardList.Add(new Card(94, "Xương Cuồng", 1, 2, 7, "[Đầm lầy]", Resources.Load<Sprite>("Yeu_Ma_Deck/Xuong_cuong_xanh"), 0, 0, 0, 0, false, 0, "Rare", ElementType.Swamp));
        cardList.Add(new Card(95, "Xương Cuồng", 1, 2, 5, "[Đầm lầy]", Resources.Load<Sprite>("Yeu_Ma_Deck/Xuong_cuong_vang"), 0, 0, 0, 0, false, 0, "Rare", ElementType.Swamp));
        cardList.Add(new Card(96, "Hồ Tinh", 3, 2, 3, "[Đầm lầy]", Resources.Load<Sprite>("Yeu_Ma_Deck/Ho_ly_tinh"), 0, 0, 0, 0, false, 0, "Common", ElementType.Swamp));
        cardList.Add(new Card(97, "Xương Cuồng", 2, 4, 6, "[Đầm lầy]", Resources.Load<Sprite>("Yeu_Ma_Deck/Xuong_cuong_tim"), 0, 0, 0, 0, false, 0, "Rare", ElementType.Swamp));
        cardList.Add(new Card(98, "Cóc băng nhào mật", 0, 0, 3, "[Đầm lầy]", Resources.Load<Sprite>("Yeu_Ma_Deck/Coc_bang"), 0, 0, 0, 0, false, 0, "Common", ElementType.Swamp));
        cardList.Add(new Card(99, "Cự Ngạ Quỷ", 5, 2, 5, "[Đầm lầy]", Resources.Load<Sprite>("Yeu_Ma_Deck/Cu_nga_quy"), 0, 0, 0, 0, false, 0, "Rare", ElementType.Swamp));
        cardList.Add(new Card(100, "Dạ xoa điên cuồng", 6, 5, 5, "[Đầm lầy]", Resources.Load<Sprite>("Yeu_Ma_Deck/Da_xoa"), 0, 0, 0, 0, false, 0, "Rare", ElementType.Swamp));
        cardList.Add(new Card(101, "Ma da", 2, 2, 1, "[Đầm lầy]", Resources.Load<Sprite>("Yeu_Ma_Deck/Ma_da"), 0, 0, 0, 0, false, 0, "Common", ElementType.Swamp));
        cardList.Add(new Card(102, "Ma hổ trành", 4, 3, 1, "[Đầm lầy]", Resources.Load<Sprite>("Yeu_Ma_Deck/Ma_ho_tranh"), 0, 0, 0, 0, false, 0, "Common", ElementType.Swamp));
        cardList.Add(new Card(103, "Ma no", 2, 1, 2, "[Đầm lầy]", Resources.Load<Sprite>("Yeu_Ma_Deck/Ma_no"), 0, 0, 0, 0, false, 0, "Common", ElementType.Swamp));
        cardList.Add(new Card(104, "Ma rác", 1, 1, 1, "[Đầm lầy]", Resources.Load<Sprite>("Yeu_Ma_Deck/Ma_rac"), 0, 0, 0, 0, false, 0, "Common", ElementType.Swamp));
        cardList.Add(new Card(105, "Ma trơi", 1, 3, 1, "[Đầm lầy]", Resources.Load<Sprite>("Yeu_Ma_Deck/Ma_troi"), 0, 0, 0, 0, false, 0, "Common", ElementType.Swamp));
        cardList.Add(new Card(106, "Ma xó", 2, 1, 1, "[Đầm lầy]", Resources.Load<Sprite>("Yeu_Ma_Deck/Ma_xo"), 0, 0, 0, 0, false, 0, "Common", ElementType.Swamp));
        cardList.Add(new Card(107, "Yêu Hồ ma quái", 4, 3, 3, "[Đầm lầy]", Resources.Load<Sprite>("Yeu_Ma_Deck/Yeu_ho"), 0, 0, 0, 0, false, 0, "Common", ElementType.Swamp));
        cardList.Add(new Card(108, "Tàn tích Mộc tinh", 2, 2, 2, "[Đầm lầy]", Resources.Load<Sprite>("Yeu_Ma_Deck/Moc_tinh"), 0, 0, 0, 0, false, 0, "Common", ElementType.Swamp));
        cardList.Add(new Card(109, "Yêu rắn khổng lồ", 3, 2, 2, "[Đầm lầy]", Resources.Load<Sprite>("Yeu_Ma_Deck/Yeu_ran"), 0, 0, 0, 0, false, 0, "Common", ElementType.Swamp));
        cardList.Add(new Card(110, "Ông ba bị", 2, 2, 3, "[Đầm lầy]", Resources.Load<Sprite>("Yeu_Ma_Deck/Ba_bi"), 0, 0, 0, 0, false, 0, "Common", ElementType.Swamp));
        cardList.Add(new Card(111, "Âu Kình", 2, 2, 6, "[Đầm lầy]", Resources.Load<Sprite>("Yeu_Ma_Deck/Au_kinh"), 0, 0, 0, 0, false, 0, "Common", ElementType.Swamp));
        cardList.Add(new Card(112, "Cây ma", 2, 2, 7, "[Đầm lầy]", Resources.Load<Sprite>("Yeu_Ma_Deck/Cay_ma"), 0, 0, 0, 0, false, 0, "Common", ElementType.Swamp));
        cardList.Add(new Card(113, "Thần trùng", 2, 2, 1, "[Đầm lầy]", Resources.Load<Sprite>("Yeu_Ma_Deck/Than_trung"), 0, 0, 0, 0, false, 0, "Common", ElementType.Swamp));
        cardList.Add(new Card(114, "Quỷ nhập tràng", 3, 1, 1, "[Đầm lầy]", Resources.Load<Sprite>("Yeu_Ma_Deck/Quy_nhap_trang"), 0, 0, 0, 0, false, 0, "Common", ElementType.Swamp));
        cardList.Add(new Card(115, "Ma lượm", 2, 1, 3, "[Đầm lầy]", Resources.Load<Sprite>("Yeu_Ma_Deck/Ma_luom"), 0, 0, 0, 0, false, 0, "Common", ElementType.Swamp));
        cardList.Add(new Card(116, "Âu Kình", 4, 1, 4, "[Đầm lầy]", Resources.Load<Sprite>("Yeu_Ma_Deck/Au_kinh_2"), 0, 0, 0, 0, false, 0, "Rare", ElementType.Swamp));
        cardList.Add(new Card(117, "Cẩu quỷ", 2, 0, 2, "[Đầm lầy]", Resources.Load<Sprite>("Yeu_Ma_Deck/Cau_quy"), 0, 0, 0, 0, false, 0, "Common", ElementType.Swamp));
        cardList.Add(new Card(118, "Ma Thần vòng", 2, 1, 2, "[Đầm lầy]", Resources.Load<Sprite>("Yeu_Ma_Deck/Ma_vong"), 0, 0, 0, 0, false, 0, "Common", ElementType.Swamp));
        cardList.Add(new Card(119, "Thiên Linh Cái", 2, 1, 2, "[Đầm lầy]", Resources.Load<Sprite>("Yeu_Ma_Deck/Thien_Linh_Cai"), 0, 0, 0, 0, false, 0, "Common", ElementType.Swamp));

        /* Lạc Điểu Deck*/
        cardList.Add(new Card(120, "Bùa sư", 1, 1, 1, "[Rừng núi]", Resources.Load<Sprite>("Lac_Dieu_Deck/Bua_su"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(121, "Đại thi sĩ", 2, 1, 2, "[Rừng núi]", Resources.Load<Sprite>("Lac_Dieu_Deck/Dai_thi_si"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(122, "Khí công pháp sư", 3, 3, 3, "[Rừng núi]", Resources.Load<Sprite>("Lac_Dieu_Deck/Khi_cong_phap_su"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(123, "Khí công sư tập sự", 1, 1, 1, "[Rừng núi]", Resources.Load<Sprite>("Lac_Dieu_Deck/Khi_cong_su"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(124, "Lạc điểu tiên phong", 2, 4, 2, "[Rừng núi]", Resources.Load<Sprite>("Lac_Dieu_Deck/Lac_dieu_tien_phong"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(125, "Lạc điểu trưởng lão", 2, 1, 5, "[Rừng núi]", Resources.Load<Sprite>("Lac_Dieu_Deck/Lac_dieu_truong_lao"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(126, "Pháp sư khoe mẽ", 3, 2, 2, "[Rừng núi]", Resources.Load<Sprite>("Lac_Dieu_Deck/Phap_su_khoe_me"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(127, "Pháp sư Lạc điểu", 3, 2, 2, "[Rừng núi]", Resources.Load<Sprite>("Lac_Dieu_Deck/Phap_su_Lac_dieu"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(128, "Thi sĩ nhỏm ho", 1, 1, 1, "[Rừng núi]", Resources.Load<Sprite>("Lac_Dieu_Deck/Thi_si_nhon_nho"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(129, "Thủ lĩnh Lạc điểu tộc ", 3, 4, 4, "[Rừng núi]", Resources.Load<Sprite>("Lac_Dieu_Deck/Thu_linh_Lac_dieu"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(130, "Thượng sư tiên đoán ", 3, 1, 3, "[Rừng núi]", Resources.Load<Sprite>("Lac_Dieu_Deck/Thuong_su_tien_doan"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(131, "Tư tế Lạc điểu", 2, 1, 2, "[Rừng núi]", Resources.Load<Sprite>("Lac_Dieu_Deck/Tu_te_Lac_dieu"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(132, "Tư La, tù trưởng Lạc điểu tộc", 3, 2, 4, "[Rừng núi]", Resources.Load<Sprite>("Lac_Dieu_Deck/Tu_truong_Lac_dieu"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(133, "Võ sĩ khôn lỏi", 1, 1, 2, "[Rừng núi]", Resources.Load<Sprite>("Lac_Dieu_Deck/Vo_si_khon_loi"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(134, "Võ sĩ đơn độc", 2, 4, 4, "[Rừng núi]", Resources.Load<Sprite>("Lac_Dieu_Deck/Vo_si_don_doc"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
        cardList.Add(new Card(135, "Vũ công Lạc điểu", 1, 1, 1, "[Rừng núi]", Resources.Load<Sprite>("Lac_Dieu_Deck/Vu_cong_Lac_dieu"), 0, 0, 0, 0, false, 0, "Common", ElementType.Forest));
    }
}
