# Hướng dẫn sử dụng AI Phase Timing

## 🎯 Vấn đề đã được giải quyết

Trước đây AI có vấn đề về timing:
- ❌ **Chuyển phase quá nhanh** từ Summon sang Attack
- ❌ **Triệu hồi và tấn công liên tục** không tự nhiên
- ❌ **Delay cố định** giữa các phase (0.7s, 0.9s)

## ✅ Giải pháp mới

### **Timing cải thiện:**
- **PostSummon Delay**: Tăng từ 0.7s lên **3s**
- **EndTurn Delay**: Tăng từ 0.9s lên **2s**
- **Phase Delay**: **2-5s** (random) giữa các phase
- **Cảm giác tự nhiên** hơn khi AI chuyển phase

## ⏱️ Cài đặt timing mới

### Trong Inspector
- **postSummonDelay**: 3f (giây) - Delay cố định từ Summon → Attack
- **endTurnDelayAfterAttack**: 2f (giây) - Delay cố định từ Attack → End
- **minPhaseDelay**: 2f (giây) - Delay tối thiểu giữa các phase
- **maxPhaseDelay**: 5f (giây) - Delay tối đa giữa các phase

### Trong Code
```csharp
// Thay đổi timing trong runtime
AI ai = FindObjectOfType<AI>();
ai.SetPhaseTiming(3f, 8f); // Đặt delay từ 3-8 giây

// Lấy thông tin timing hiện tại
string timingInfo = ai.GetPhaseTimingInfo();
Debug.Log(timingInfo);
```

## 🚀 Cách hoạt động

### 1. **Summon Phase → Attack Phase**
- **Trước đây**: Chuyển ngay sau 0.7s
- **Bây giờ**: Chuyển sau **2-5s** (random)
- **Log**: `[AI] Attack Phase activated after 3.7 seconds delay`

### 2. **Attack Phase → End Phase**
- **Trước đây**: Chuyển ngay sau 0.9s
- **Bây giờ**: Chuyển sau **2-5s** (random)
- **Log**: `[AI] End Phase activated after 4.2 seconds delay`

### 3. **Timing tổng thể**
```
Draw Phase → Summon Phase → [2-5s delay] → Attack Phase → [2-5s delay] → End Phase
```

## ⚙️ Tùy chỉnh timing

### Thay đổi delay cơ bản
```csharp
// Trong Inspector
minPhaseDelay = 1f;    // AI chuyển phase nhanh hơn
maxPhaseDelay = 3f;    // AI chuyển phase nhanh hơn

// Hoặc
minPhaseDelay = 5f;    // AI chuyển phase chậm hơn
maxPhaseDelay = 10f;   // AI chuyển phase chậm hơn
```

### Thay đổi delay trong runtime
```csharp
// Đặt timing nhanh cho AI dễ
ai.SetPhaseTiming(1f, 3f);

// Đặt timing chậm cho AI khó
ai.SetPhaseTiming(5f, 10f);

// Đặt timing cố định
ai.SetPhaseTiming(3f, 3f); // Luôn chờ đúng 3 giây
```

### Timing theo từng loại AI
```csharp
switch (aiType)
{
    case AIType.ThuyTinh:
        SetPhaseTiming(1f, 3f);   // AI dễ - chuyển phase nhanh
        break;
    case AIType.SonTinh:
        SetPhaseTiming(2f, 5f);   // AI trung bình
        break;
    case AIType.YeuMa:
        SetPhaseTiming(3f, 7f);   // AI khó - chuyển phase chậm
        break;
    case AIType.LacDieu:
        SetPhaseTiming(5f, 10f);  // AI rất khó - chuyển phase rất chậm
        break;
}
```

## 🎮 Trải nghiệm người chơi

### **Trước đây:**
- AI triệu hồi và tấn công liên tục
- Cảm giác không tự nhiên
- Game quá nhanh và khó theo dõi

### **Bây giờ:**
- AI có thời gian "suy nghĩ" giữa các phase
- Cảm giác tự nhiên và thực tế
- Người chơi có thời gian quan sát và chuẩn bị
- Game cân bằng và dễ theo dõi hơn

## 📊 Ví dụ timing

| Tình huống | Min Delay | Max Delay | Mô tả |
|------------|------------|-----------|-------|
| **AI dễ** | 1s | 3s | AI phản ứng nhanh |
| **AI trung bình** | 2s | 5s | AI cân bằng |
| **AI khó** | 3s | 7s | AI thận trọng |
| **AI rất khó** | 5s | 10s | AI rất thận trọng |

## 🔧 Troubleshooting

### AI vẫn chuyển phase quá nhanh
```csharp
// Tăng delay
ai.SetPhaseTiming(5f, 10f);
```

### AI chuyển phase quá chậm
```csharp
// Giảm delay
ai.SetPhaseTiming(1f, 3f);
```

### Timing không hoạt động
- Kiểm tra `minPhaseDelay` và `maxPhaseDelay` trong Inspector
- Đảm bảo `minPhaseDelay <= maxPhaseDelay`
- Kiểm tra Console để xem log

## 🌟 Tips sử dụng

1. **Timing phù hợp với độ khó AI:**
   - AI dễ: 1-3 giây
   - AI trung bình: 2-5 giây  
   - AI khó: 3-7 giây

2. **Cân bằng gameplay:**
   - Delay quá ngắn: AI quá nhanh
   - Delay quá dài: Game chậm chạp
   - Delay vừa phải: Game cân bằng

3. **Tùy chỉnh theo người chơi:**
   - Người chơi mới: Delay ngắn hơn
   - Người chơi kinh nghiệm: Delay dài hơn

## 📝 Ví dụ hoàn chỉnh

```csharp
public class AITimingManager : MonoBehaviour
{
    [Header("AI Timing Settings")]
    [SerializeField] private float easyMinDelay = 1f;
    [SerializeField] private float easyMaxDelay = 3f;
    [SerializeField] private float mediumMinDelay = 2f;
    [SerializeField] private float mediumMaxDelay = 5f;
    [SerializeField] private float hardMinDelay = 3f;
    [SerializeField] private float hardMaxDelay = 7f;
    
    public void SetAITiming(AIType aiType)
    {
        AI ai = FindObjectOfType<AI>();
        
        switch (aiType)
        {
            case AIType.ThuyTinh:
                ai.SetPhaseTiming(easyMinDelay, easyMaxDelay);
                break;
            case AIType.SonTinh:
                ai.SetPhaseTiming(mediumMinDelay, mediumMaxDelay);
                break;
            case AIType.YeuMa:
                ai.SetPhaseTiming(hardMinDelay, hardMaxDelay);
                break;
        }
    }
}
```

## 🎯 Kết quả mong đợi

Sau khi áp dụng timing mới:
- ✅ AI không còn triệu hồi và tấn công liên tục
- ✅ Có thời gian hợp lý giữa các phase
- ✅ Cảm giác tự nhiên và thực tế hơn
- ✅ Game cân bằng và dễ theo dõi
- ✅ Người chơi có thời gian chuẩn bị

---

**Chúc bạn có trải nghiệm game cân bằng và thú vị hơn với AI Phase Timing! 🎮✨**
