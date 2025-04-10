using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDatabase : MonoBehaviour
{
    public static List<Card> cardList = new List<Card>();

    void Awake()
    {
        /* Lac Viet Deck */
        //Creature card
        cardList.Add(new Card(0, "None", 0, 0, 0,"None", Resources.Load<Sprite>("Au_Co"), 0, 0, 0, 0, false, 0));
        cardList.Add(new Card(1, "Lạc Long Quân", 2, 6, 6,"Thêm 2 lá bài lên tay", Resources.Load<Sprite>("Lac_Viet_Deck/Lac_Long_Quan"), 2, 0, 0, 0, false, 0));
        cardList.Add(new Card(2, "Cờ Binh Lạc Việt", 3, 3, 3," ", Resources.Load<Sprite>("Lac_Viet_Deck/Co_binh"), 0, 0, 0, 0, false, 0));
        cardList.Add(new Card(3, "Cung thủ Lạc Việt", 2, 1, 1," ", Resources.Load<Sprite>("Lac_Viet_Deck/Cung_thu"), 0, 0, 0, 0, false, 0));
        cardList.Add(new Card(4, "Dũng tướng tiên phong", 4, 3, 3," ", Resources.Load<Sprite>("Lac_Viet_Deck/Dung_tuong"), 0, 0, 0, 0, false, 0));
        cardList.Add(new Card(5, "Giáp sắt", 4, 4, 4," ", Resources.Load<Sprite>("Lac_Viet_Deck/Giap_sat"), 0, 0, 0, 0, false, 0));
        cardList.Add(new Card(6, "Khiên thủ Lạc Việt", 2, 3, 3," ", Resources.Load<Sprite>("Lac_Viet_Deck/Khien_thu"), 0, 0, 0, 0, false, 0));
        cardList.Add(new Card(7, "Kỵ binh xông xáo", 2, 3, 1," ", Resources.Load<Sprite>("Lac_Viet_Deck/Ky_binh"), 0, 0, 0, 0, false, 0));
        cardList.Add(new Card(8, "Lính tiên phong", 1, 1, 1," ", Resources.Load<Sprite>("Lac_Viet_Deck/Linh_tien_phong"), 0, 0, 0, 0, false, 0));
        cardList.Add(new Card(9, "Lạc tướng khôn ngoan", 3, 3, 3," ", Resources.Load<Sprite>("Lac_Viet_Deck/Lac_tuong"), 0, 0, 0, 0, false, 0));
        cardList.Add(new Card(10, "Lương Khan-Chiến thần Lạc Việt", 3, 1, 3," ", Resources.Load<Sprite>("Lac_Viet_Deck/Luong_Khan"), 0, 0, 0, 0, false, 0));
        cardList.Add(new Card(11, "Lạc Hầu tận tụy", 3, 1, 3," ", Resources.Load<Sprite>("Lac_Viet_Deck/Lac_Hau"), 0, 0, 0, 0, false, 0));
        cardList.Add(new Card(12, "Mũ sắt", 2, 3, 3," ", Resources.Load<Sprite>("Lac_Viet_Deck/Mu_sat"), 0, 0, 0, 0, false, 0));
        cardList.Add(new Card(13, "Ngựa sắt", 5, 5, 5," ", Resources.Load<Sprite>("Lac_Viet_Deck/Ngua_sat"), 0, 0, 0, 0, false, 0));
        cardList.Add(new Card(14, "Roi sắt", 3, 4, 4," ", Resources.Load<Sprite>("Lac_Viet_Deck/Roi_sat"), 0, 0, 0, 0, false, 0));
        cardList.Add(new Card(15, "Sứ giả Lạc Việt", 2, 3, 3," ", Resources.Load<Sprite>("Lac_Viet_Deck/Su_gia"), 0, 0, 0, 0, false, 0));
        cardList.Add(new Card(16, "Thần rèn Lạc Việt", 3, 4, 4," ", Resources.Load<Sprite>("Lac_Viet_Deck/Than_ren"), 0, 0, 0, 0, false, 0));
        cardList.Add(new Card(17, "Thợ rèn Lạc Việt", 2, 1, 2," ", Resources.Load<Sprite>("Lac_Viet_Deck/Tho_ren"),0, 0, 0, 0, false, 0));
        cardList.Add(new Card(18, "Kẻ cuồng Long tộc", 2, 2, 2," ", Resources.Load<Sprite>("Lac_Viet_Deck/Ke_cuong_Long_toc"), 0, 0, 0, 0, false, 0));
        cardList.Add(new Card(19, "Cồng binh Lạc Việt", 1, 2, 2," ", Resources.Load<Sprite>("Lac_Viet_Deck/Cong_binh"), 0, 0, 0, 0, false, 0));
        cardList.Add(new Card(20, "Sơ cứu nhiệt tình", 2, 1, 2," ", Resources.Load<Sprite>("Lac_Viet_Deck/So_cuu_nhiet_tinh"), 0, 0, 0, 0, false, 0));
        cardList.Add(new Card(21, "Gióng", 2, 1, 5," ", Resources.Load<Sprite>("Lac_Viet_Deck/Giong"), 0, 0, 0, 0, false, 0));

        //Spell Card
        cardList.Add(new Card(22, "Chiến nỏ", 1, 0, 0," ", Resources.Load<Sprite>("Lac_Viet_Deck/Chien_no"), 0, 0, 0, 0, true, 1));
        cardList.Add(new Card(23, "Tĩnh dưỡng", 1, 0, 0," ", Resources.Load<Sprite>("Lac_Viet_Deck/Tinh_duong"), 0, 2, 0, 2, true, 0));
        cardList.Add(new Card(24, "Hố chông", 1, 0, 0," ", Resources.Load<Sprite>("Lac_Viet_Deck/Ho_chong"), 0, 0, 0, 0, true, 2));
        // cardList.Add(new Card(25, "Đồng lòng chiến đấu", 1, 0, 0," ", Resources.Load<Sprite>("Lac_Viet_Deck/Dong_long_chien_dau"), 0, 0, 2, 0, true, 0));
        // cardList.Add(new Card(26, "Bắt cóc", 1, 0, 0," ", Resources.Load<Sprite>("Lac_Viet_Deck/Bat_coc"), 0, 0, 1, 0, true, 0));
        cardList.Add(new Card(25, "Công thành bất ngờ", 1, 0, 0," ", Resources.Load<Sprite>("Lac_Viet_Deck/Cong_thanh"), 0, 0, 0, 0, true, 3));
        cardList.Add(new Card(26, "Liên hoàn sấm sét", 1, 0, 0," ", Resources.Load<Sprite>("Lac_Viet_Deck/Sam_set"), 0, 0, 0, 0, true, 4));
        cardList.Add(new Card(27, "Bất hoại", 1, 0, 0," ", Resources.Load<Sprite>("Lac_Viet_Deck/Bat_hoai"), 0, 0, 0, 3, true, 0));
        cardList.Add(new Card(28, "Trao kiếm", 1, 0, 0," ", Resources.Load<Sprite>("Lac_Viet_Deck/Trao_kiem"), 2, 0, 0, 0, true, 0));
        cardList.Add(new Card(29, "Chiến trường không khoan nhượng", 1, 0, 0," ", Resources.Load<Sprite>("Lac_Viet_Deck/Chien_truong"), 0, 0, 0, -3, true, 5));
        cardList.Add(new Card(30, "Chiến trường không khoan nhượng", 1, 0, 0," ", Resources.Load<Sprite>("Lac_Viet_Deck/Chien_truong"), 0, 0, 0, -3, true, 5));
        cardList.Add(new Card(31, "Chiến trường không khoan nhượng", 1, 0, 0," ", Resources.Load<Sprite>("Lac_Viet_Deck/Chien_truong"), 0, 0, 0, -3, true, 5));
        cardList.Add(new Card(32, "Chiến trường không khoan nhượng", 1, 0, 0," ", Resources.Load<Sprite>("Lac_Viet_Deck/Chien_truong"), 0, 0, 0, -3, true, 5));
        cardList.Add(new Card(33, "Chiến trường không khoan nhượng", 1, 0, 0," ", Resources.Load<Sprite>("Lac_Viet_Deck/Chien_truong"), 0, 0, 0, -3, true, 5));
        cardList.Add(new Card(34, "Chiến trường không khoan nhượng", 1, 0, 0," ", Resources.Load<Sprite>("Lac_Viet_Deck/Chien_truong"), 0, 0, 0, -3, true, 5));
        cardList.Add(new Card(35, "Chiến trường không khoan nhượng", 1, 0, 0," ", Resources.Load<Sprite>("Lac_Viet_Deck/Chien_truong"), 0, 0, 0, -3, true, 5));
        cardList.Add(new Card(36, "Chiến trường không khoan nhượng", 1, 0, 0," ", Resources.Load<Sprite>("Lac_Viet_Deck/Chien_truong"), 0, 0, 0, -3, true, 5));
        cardList.Add(new Card(37, "Chiến trường không khoan nhượng", 1, 0, 0," ", Resources.Load<Sprite>("Lac_Viet_Deck/Chien_truong"), 0, 0, 0, -3, true, 5));
        cardList.Add(new Card(38, "Chiến trường không khoan nhượng", 1, 0, 0," ", Resources.Load<Sprite>("Lac_Viet_Deck/Chien_truong"), 0, 0, 0, -3, true, 5));
        cardList.Add(new Card(39, "Chiến trường không khoan nhượng", 1, 0, 0," ", Resources.Load<Sprite>("Lac_Viet_Deck/Chien_truong"), 0, 0, 0, -3, true, 5));
        cardList.Add(new Card(40, "Chiến trường không khoan nhượng", 1, 0, 0," ", Resources.Load<Sprite>("Lac_Viet_Deck/Chien_truong"), 0, 0, 0, -3, true, 5));
    }
}
