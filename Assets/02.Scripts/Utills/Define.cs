
public class Define
{
    public enum PokeType
    {
        None,
        Normal,
        Fighting,
        Poison,
        Ground,
        Flying,
        Bug,
        Rock,
        Ghost,
        Steel,
        Fire,
        Water,
        Electric,
        Grass,
        Ice,
        Psychic,
        Dragon,
        Dark,
        Fairy
    }

    public enum PokeRarity
    {
        Common, //�ù� ������ ���̳����� ���� 
        Rare,   // �幮 Ȯ���� ������ ����
        unique, //��ģ Ȯ���� ������ ����
        Legendary, //�� �Ѹ����� ���� �����ϴ� ����
    }

    public enum Scene
    {
        Unknown,
        Map,
        Battle,
    }

    public enum Sound
    {
        Effect,
        Bgm,
        MaxCount,
    }

    public enum UIEvent
    {
        Click,
        Drag,
    }
}