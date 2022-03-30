using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resurrection : MonoBehaviour
{
    public float forceJump;                                         // переменная для силы прыжка

    public void OnCollisionEnter2D(Collision2D collision)           // столкновения
    {
        if (collision.relativeVelocity.y < 0)                       // если относительная скорость меньше 0, ну вниз летит короче
        {
            Doodle.instance.DoodleRigid.velocity = Vector2.up * forceJump;  // добавляем прыжок к переменной из скрипта "Doodle"
        }        
    }

    //public void OnCollisionExit2D(Collision2D collision)            // столкновения (ну только это когда оно заканчивается)
   // {
     //   if (collision.collider.name == "DeadZone")                  // если платформа встретилась с объектом с именем DeadZone
     //   {
     //       float RandX = Random.Range(-1.7f, 1.7f);                // то задаем новую позицию по х
      //      float RandY = Random.Range(transform.position.y + 20f, transform.position.y + 22f); // и новую позицию по у
      //
      //      transform.position = new Vector3(RandX, RandY, 0);      // перемещаем объект по заданным координатам
      //  }
  //  }
    
}
