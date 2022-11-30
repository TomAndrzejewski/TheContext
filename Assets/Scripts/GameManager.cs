using UnityEngine;
using DialogueEditor;
using System.Collections.Generic;
using System.Linq;
using Stateless;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private NPCConversation npcConversation;

    public List<ThiefController> thiefControllers;
    public List<PolicemanController> policemanControllers;

    private State state = State.s0;
    private StateMachine<State, Choice> stateMachine;

    public Sprite amongus;
    
    public void Start()
    {
        stateMachine = new(() => state, s => state = s);

        stateMachine.Configure(State.s0)
            .Permit(Choice.TwoAtVault, State.s1)
            .Permit(Choice.OneAtVault, State.s2);

        stateMachine.Configure(State.s2)
            .OnEntryFrom(Choice.OneAtVault, () => SendThiefsToBank(false))
            .Permit(Choice.WinkAtMage, State.s6)
            .Permit(Choice.Shoot, State.s5);

        stateMachine.Configure(State.s1)
            .OnEntryFrom(Choice.TwoAtVault, () => SendThiefsToBank(true))
            .Permit(Choice.GasInVault, State.s4)
            .Permit(Choice.Shoot, State.s3);

        stateMachine.Configure(State.s6)
            .OnEntryFrom(Choice.WinkAtMage, () => WinkAtMage());

        stateMachine.Configure(State.s5)
            .OnEntryFrom(Choice.Shoot, () => Shoot2());

        stateMachine.Configure(State.s4)
           .OnEntryFrom(Choice.GasInVault, () => ReleaseGas());

        stateMachine.Configure(State.s3)
           .OnEntryFrom(Choice.Shoot, () => Shoot1());

        ConversationManager.Instance.StartConversation(npcConversation);

        policemanControllers[0].OnReachedDestination += () =>
        {
            SetDeadSprite(policemanControllers[0].gameObject);
            SetDeadSprite(thiefControllers[2].gameObject);
            thiefControllers[2].SetNewTargetPosition(thiefControllers[2].transform);
        };

    }

    void Update()
    {
        if(!thiefControllers.Any(tc => tc.IsMoving) && !policemanControllers.All(pc => pc.IsMoving))
        {
            SetCanClick(true);
        }
    }

    public static void SetCanClick(bool canClick)
    {
        ConversationManager.Instance.AllowMouseInteraction = canClick;
    }

    public void Trigger(int choiceInt)
    {
        var choice = (Choice)choiceInt;
        Debug.Log($"trigger {choice}");
        stateMachine.Fire(choice);
    }

    private void SendThiefsToBank(bool watchClerk1)
    {
        Debug.Log("Ruszcie dupe");
        thiefControllers[0].GoToBankEntrace(false);
        thiefControllers[1].GoToBankEntrace(watchClerk1);
        thiefControllers[2].GoToBankEntrace(false);
    }

    private void Shoot1()
    {
        Debug.Log("strzelam w jednego");

        policemanControllers[0].gameObject.SetActive(true);
        policemanControllers[0].EnterBank();
        SetDeadSprite(thiefControllers[0].gameObject);
        thiefControllers[1].Run();
        thiefControllers[2].Run();

    }

    private void Shoot2()
    {
        Debug.Log("strzelam w dwoch");
    }
    private void ReleaseGas()
    {
        Debug.Log("wypuszczam gaz");
    }

    private void WinkAtMage()
    {
        Debug.Log("mrug mrug");
        SetDeadSprite(thiefControllers[0].gameObject);
        SetDeadSprite(thiefControllers[1].gameObject);
        thiefControllers[2].Run();
        policemanControllers[0].gameObject.SetActive(true);
        policemanControllers[1].gameObject.SetActive(true);
        policemanControllers[2].gameObject.SetActive(true);
        policemanControllers[0].EnterBank();
        policemanControllers[1].EnterBank();
        policemanControllers[2].EnterBank();
    }

    public void SetDeadSprite(GameObject gameObject)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = amongus;
    }

   
}
