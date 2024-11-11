using UnityEngine;

public class TitleBounceEffect : MonoBehaviour
{
    public float bounceAmplitude = 10f; // Độ cao của chuyển động lên xuống
    public float bounceFrequency = 2f; // Tần số của chuyển động

    private Vector3 initialPosition;

    void Start()
    {
        // Lưu vị trí ban đầu của đối tượng
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        // Tạo chuyển động lên xuống theo dạng sóng sin
        float newY = Mathf.Sin(Time.time * bounceFrequency) * bounceAmplitude;
        transform.localPosition = initialPosition + new Vector3(0, newY, 0);
    }
}
