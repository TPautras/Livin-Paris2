using System;
using System.Windows.Input;
using LivinParis_Graphique.Core;
using SqlConnector.DataAccess;
using SqlConnector.Models;

namespace LivinParis_Graphique.MVVM.ViewModel
{
    public class ReviewViewModel : BaseViewModel
    {
        private string _review;
        private string _note;

        public string Review
        {
            get { return _review;}
            set { _review=value;OnPropertyChanged();}
        }
        public string Note
        {
            get { return _note;}
            set { _note=value;OnPropertyChanged();}
        }
        public ICommand CreerReview { get; set; }
        public ClientDetailCommandeViewModel ClientDetailCommandeViewModel { get; set; }
        public ReviewViewModel(ClientDetailCommandeViewModel clientDetailCommandeViewModel)
        {
            CreerReview = new RelayCommand(o => CreerReviewExecute());
            ClientDetailCommandeViewModel = clientDetailCommandeViewModel;
        }

        public void CreerReviewExecute()
        {
            Evaluation evaluation = new Evaluation{
                CommandeId = ClientDetailCommandeViewModel.Commande.CommandeId,
                EvaluationClient = Convert.ToDecimal(Note),
                EvaluationDescriptionClient = Review,
                EvaluationCuisinier = 0,
                EvaluationDescriptionCuisinier = "",
                EvaluationId = new EvaluationDataAccess().GetAll().Count+1
            };
            Console.WriteLine(evaluation.EvaluationClient);
            new EvaluationDataAccess().Insert(evaluation);
            
        }
    }
}