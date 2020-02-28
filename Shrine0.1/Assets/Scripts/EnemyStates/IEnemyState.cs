using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//defines some states (maybe enemy has run state that changes speed etc..)
public interface IEnemyState
{
    //prepare the state
    void Enter(Enemy parent);

    //update the state
    void Update();

    //exit the state (change back to default value)
    void Exit();

}
