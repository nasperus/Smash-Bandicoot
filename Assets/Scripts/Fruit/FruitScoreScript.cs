using UnityEngine;
using TMPro;

namespace Fruit
{
    public class FruitScoreScript : MonoBehaviour
    {
       
        [SerializeField] private TextMeshProUGUI scoreText;
        private int _increaseScore;
        

        private void Start()
        {
            if (scoreText == null)
            {
                scoreText = FindObjectOfType<TextMeshProUGUI>();
            }
            _increaseScore = 0;

        }

        public void IncreaseScore()
        {
            _increaseScore++;
            scoreText.text = _increaseScore.ToString();
            
            
        }
        
    }
}