 using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapScene : BaseScene
{
    private CameraController _cc;

    private bool isPanelOn = false;

    private GameInfo _gameInfo = new GameInfo();
    private AgentInfo _agentInfo = new AgentInfo();
    private Pokemon _pokemon = new Pokemon();

    [SerializeField] private List<Text> pokeName;
    [SerializeField] private List<Image> pokeImage;
    [SerializeField] private List<Text> pokeLevel;
    [SerializeField] private List<Text> pokeHP;
    [SerializeField] private GameObject pokeInfoPanel;
    [SerializeField] private List<GameObject> pokeInfoPanels;

    private Player _player;
    public Player Player => _player;

    [SerializeField]
    private TextPanel _textPanel;

    [SerializeField]
    private List<Enemy> _trainerList;
    [SerializeField] private GameObject panel;
    [SerializeField] private Button continueBtn;
    [SerializeField] private Button quitBtn;

    protected override void Init()
    {
        base.Init();

        _gameInfo = Managers.Save.LoadJsonFile<GameInfo>();

        continueBtn.onClick.AddListener(ContinueGame);
        quitBtn.onClick.AddListener(AplicationQuit);
        pokeInfoPanel.SetActive(false);

        ContinueGame();

        SceneType = Define.Scene.Map;

        _gameInfo = Managers.Save.LoadJsonFile<GameInfo>();

        _player = Managers.Resource.Instantiate("Player/visugi").GetComponent<Player>();

        _player.SetInfo(_gameInfo.PlayerInfo);
        _player.transform.position = new Vector3(_gameInfo.PlayerInfo.position.x, _gameInfo.PlayerInfo.position.y, _gameInfo.PlayerInfo.position.z);

        foreach(var t in _trainerList)
        {
            t.transform.Find("AI").GetComponent<AIBrain>().SetTarget(_player.gameObject);
        }

        _cc = Camera.main.GetComponent<CameraController>();
        _cc.SetTarget(_player.gameObject);

        _textPanel.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pokeInfoPanel.SetActive(false);
            StopGame();
        }

        if(Input.GetKeyDown(KeyCode.I))
        {
            isPanelOn = !isPanelOn;
            pokeInfoPanel.SetActive(isPanelOn);
            allFalse();
            PokemonInfoLoad();
        }
    }

    public void SetText(string msg)
    {
        _textPanel.gameObject.SetActive(true);
        _textPanel.SetText(msg);
    }

    public override void Clear()
    {
        //if(_player != null)
        //{
        //    Managers.Pool.Push(_player.GetComponent<Poolable>());
        //    _player = null;
        //}
    }

    void StopGame()
    {
        Time.timeScale = 0f;
        panel.SetActive(true);
    }

    void ContinueGame()
    {
        Time.timeScale = 1f;
        panel.SetActive(false);
    }

    void AplicationQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void PokemonInfoLoad()
    {
        _agentInfo = _gameInfo.PlayerInfo;
        Debug.Log(_agentInfo.PokemonList[0].Name);
        int i = 0;
        foreach (Pokemon poke in _agentInfo.PokemonList)
        {
            if (poke.Name.Length > 1)
            {
                Debug.Log(poke.Name);
                pokeInfoPanels[i].SetActive(true);
                pokeName[i].text = poke.Name;
                //if(poke.Info == null)
                //{
                //    Debug.Log("info 없음");
                //}

                pokeImage[i].sprite = poke.Image;
                pokeLevel[i].text = $"Lv{poke.Level.ToString()}";
                pokeHP[i].text = $"{poke.Hp} / {poke.MaxHp}";
            }
            //널이면 없음 인포 띄우기;
            i++;
        }
    }

    void allFalse()
    {
        for(int i = 0; i < 6; i++)
        {
            pokeInfoPanels[i].SetActive(false);
        }
    }
}
