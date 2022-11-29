
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
        Common, //시발 ㅈ같이 많이나오는 몬스터 
        Rare,   // 드문 확률로 나오는 몬스터
        unique, //미친 확률로 나오는 몬스터
        Legendary, //단 한마리만 따로 출현하는 몬스터
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