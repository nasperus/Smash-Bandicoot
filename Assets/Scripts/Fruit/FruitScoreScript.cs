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
            _increaseScore = 0;

        }

        public void IncreaseScore()
        {
            scoreText.text = _increaseScore.ToString();
            _increaseScore++;
            
        }
        
    }
}