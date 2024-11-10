using UnityEngine;

public class CardMatchEffectController : MonoBehaviour
{
    public ParticleSystem sparkleEffectPrefab; // Prefab của Particle System

    // Phương thức tạo hiệu ứng riêng biệt tại mỗi vị trí
    public void PlayEffectAtPosition(Vector3 position)
    {
        // Tạo bản sao của Particle System tại vị trí chỉ định
        ParticleSystem sparkle = Instantiate(sparkleEffectPrefab, position, Quaternion.identity);
        sparkle.Play();

        // Hủy bản sao sau khi hiệu ứng kết thúc
        Destroy(sparkle.gameObject, sparkle.main.duration);
    }
}
