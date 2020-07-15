using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EcoMundi.Data
{
    [CreateAssetMenu(fileName = "GameLocalDatabase", menuName = "EcoMundi/Databases/GameLocalDatabase", order = 50)]
    public class GameLocalDatabase : ScriptableObject
    {
        [Header("PROVINCES")]
        public List<ProvincePropsData> provinceDatabase;

        [Header("QUIZZES")]
        public List<QuizData> quizDatabase;
    }

    #region [-----     PROVINCES     -----]

    [Serializable]
    public class ProvincePropsData
    {
        public E_Province province;
        public GameObject symbolPrefab;
    }

    public enum E_Province
    {
        None,

        Galapagos,

        Esmeraldas,
        Manabi,
        SantaElena,
        LosRios,
        Guayas,
        ElOro,
        SantoDomingo,

        Pichincha,
        Tungurahua,
        Cotopaxi,
        Carchi,
        Chimborazo,
        Imbabura,
        Loja,
        Bolivar,
        Azuay,
        Cañar,

        Sucumbios,
        MoronaSantiago,
        Napo,
        ZamoraChinchipe,
        Orellana,
        Pastaza
    }

    #endregion

    #region [-----     QUIZ     -----]

    [Serializable]
    public class QuizData
    {
        [TextArea(2,4)]
        public string question;
        public E_QuizType quizType;
        public List<AnswerData> answerOptions;
        [TextArea(2,4)]
        public string answerFeedback;
    }

    [Serializable]
    public class AnswerData
    {
        [TextArea(2,4)]
        public string answer;
        public bool isCorrect;
    }

    public enum E_QuizType
    {
        Home,
        Institute,
        Mall,
        General,
        Transport
    }

    #endregion

}
