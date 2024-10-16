using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LikeSpawner : MonoBehaviour
{
    public GameObject likePrefab; 
    public RectTransform canvasRect; 
    public float spawnRangeX = 200f; 
    public float spawnRangeY = 200f; 
    public float likeLifetime = 2.0f;
  
    public void SpawnLike()
    {
       
        GameObject newLike = Instantiate(likePrefab, canvasRect);

        RectTransform likeRect = newLike.GetComponent<RectTransform>();

      
        likeRect.anchoredPosition = new Vector2(
            Random.Range(-canvasRect.rect.width / 2, canvasRect.rect.width / 2),
            Random.Range(-canvasRect.rect.height / 2, canvasRect.rect.height / 2)
        );

        StartCoroutine(DestroyLikeAfterDelay(newLike, likeLifetime));
    }

    private IEnumerator DestroyLikeAfterDelay(GameObject like, float delay)
    {
        yield return new WaitForSeconds(delay);


        Animator likeAnimator = like.GetComponent<Animator>();
        if (likeAnimator != null)
        {
            likeAnimator.SetTrigger("disappear");
            AnimatorStateInfo stateInfo = likeAnimator.GetCurrentAnimatorStateInfo(0);
            float animDuration = stateInfo.length;  
            yield return new WaitForSeconds(animDuration);
        }
        Destroy(like);
    }
}
