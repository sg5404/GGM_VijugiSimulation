
public class Define
{
    public enum PokeType
    {
        None,
        Normal,
        Fire,
        Grass,
        Water,
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