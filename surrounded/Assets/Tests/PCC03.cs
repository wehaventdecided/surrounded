using UnityEngine;
using Player;
using GameControl;
using NUnit.Framework;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class PCC03 : InputTestFixture
    {

        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene("Scenes/Game");
        }
        // A UnityTest behaves like a coroutine in PlayMode
        // and allows you to yield null to skip a frame in EditMode
        [UnityEngine.TestTools.UnityTest]
        public System.Collections.IEnumerator PCC03TestWithEnumeratorPasses()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Assert.That(player, !Is.Null);
            GameObject bullet = GameObject.Instantiate(Resources.Load("Prefab/PiercingBullet"),
                new Vector3(player.transform.position.x, player.transform.position.y + 3), Quaternion.identity) as GameObject;
            Assert.That(bullet, !Is.Null);
            bullet.GetComponent<Bullet>().damage = 1;
            bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 4, ForceMode2D.Impulse);
            yield return new WaitForSeconds(4);
            Assert.That(player.GetComponent<PlayerController>().health 
                        < player.GetComponent<PlayerController>().stats.maxHealth, Is.True);
            yield return true;
        }
    }
}