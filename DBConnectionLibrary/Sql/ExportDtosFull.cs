using System;
using SqlConnector.Models;

public class ClientExportDto
{
    public string ClientUsername { get; set; }
    public string ClientPassword { get; set; }
    public string PersonneEmail { get; set; }
}

public static class ClientExportDtoMapper
{
    public static ClientExportDto FromModel(Client client)
    {
        return new ClientExportDto
        {
            ClientUsername = client.ClientUsername,
            ClientPassword = client.ClientPassword,
            PersonneEmail = client.PersonneEmail,
        };
    }
}


public class CommandeExportDto
{
    public int CommandeId { get; set; }
    public int? EntrepriseId { get; set; }
    public string CuisinierUsername { get; set; }
    public string ClientUsername { get; set; }
    public string DateCreation { get; set; }
}

public static class CommandeExportDtoMapper
{
    public static CommandeExportDto FromModel(Commande commande)
    {
        return new CommandeExportDto
        {
            CommandeId = commande.CommandeId,
            EntrepriseId = commande.EntrepriseId ?? -1,
            CuisinierUsername = commande.CuisinierUsername,
            ClientUsername = commande.ClientUsername,
            DateCreation = commande.DateCreation.ToString(),
        };
    }
}


public class CompositionDuPlatExportDto
{
    public int PlatId { get; set; }
    public int IngredientId { get; set; }
}

public static class CompositionDuPlatExportDtoMapper
{
    public static CompositionDuPlatExportDto FromModel(CompositionDuPlat compositionDuPlat)
    {
        return new CompositionDuPlatExportDto
        {
            PlatId = compositionDuPlat.PlatId,
            IngredientId = compositionDuPlat.IngredientId,
        };
    }
}


public class CompositionDeLaRecetteExportDto
{
    public int IngredientId { get; set; }
    public int RecetteId { get; set; }
}

public static class CompositionDeLaRecetteExportDtoMapper
{
    public static CompositionDeLaRecetteExportDto FromModel(CompositionDeLaRecette compositionDeLaRecette)
    {
        return new CompositionDeLaRecetteExportDto
        {
            IngredientId = compositionDeLaRecette.IngredientId,
            RecetteId = compositionDeLaRecette.RecetteId,
        };
    }
}


public class ContientExportDto
{
    public int CommandeId { get; set; }
    public int PlatId { get; set; }
}

public static class ContientExportDtoMapper
{
    public static ContientExportDto FromModel(Contient contient)
    {
        return new ContientExportDto
        {
            CommandeId = contient.CommandeId,
            PlatId = contient.PlatId,
        };
    }
}


public class CreationExportDto
{
    public int CommandeId { get; set; }
    public int PlatId { get; set; }
}

public static class CreationExportDtoMapper
{
    public static CreationExportDto FromModel(Creation creation)
    {
        return new CreationExportDto
        {
            CommandeId = creation.CommandeId,
            PlatId = creation.PlatId,
        };
    }
}


public class CuisinierExportDto
{
    public string CuisinierUsername { get; set; }
    public string CuisinierPassword { get; set; }
    public string PersonneEmail { get; set; }
}

public static class CuisinierExportDtoMapper
{
    public static CuisinierExportDto FromModel(Cuisinier cuisinier)
    {
        return new CuisinierExportDto
        {
            CuisinierUsername = cuisinier.CuisinierUsername,
            CuisinierPassword = cuisinier.CuisinierPassword,
            PersonneEmail = cuisinier.PersonneEmail,
        };
    }
}


public class EntrepriseExportDto
{
    public int EntrepriseId { get; set; }
    public string EntrepriseNom { get; set; }
}

public static class EntrepriseExportDtoMapper
{
    public static EntrepriseExportDto FromModel(Entreprise entreprise)
    {
        return new EntrepriseExportDto
        {
            EntrepriseId = entreprise.EntrepriseId,
            EntrepriseNom = entreprise.EntrepriseNom,
        };
    }
}


public class EvaluationExportDto
{
    public decimal EvaluationId { get; set; }
    public decimal EvaluationClient { get; set; }
    public decimal EvaluationCuisinier { get; set; }
    public decimal EvaluationDescriptionClient { get; set; }
    public decimal EvaluationDescriptionCuisinier { get; set; }
    public int CommandeId { get; set; }
}

public static class EvaluationExportDtoMapper
{
    public static EvaluationExportDto FromModel(Evaluation evaluation)
    {
        return new EvaluationExportDto
        {
            EvaluationId = evaluation.EvaluationId,
            EvaluationClient = evaluation.EvaluationClient,
            EvaluationCuisinier = evaluation.EvaluationCuisinier,
            EvaluationDescriptionClient = Convert.ToDecimal(evaluation.EvaluationDescriptionClient),
            EvaluationDescriptionCuisinier = Convert.ToDecimal(evaluation.EvaluationDescriptionCuisinier),
            CommandeId = evaluation.CommandeId,
        };
    }
}


public class FaitPartieDeExportDto
{
    public int PersonneId { get; set; }
    public int EntrepriseId { get; set; }
}

public static class FaitPartieDeExportDtoMapper
{
    public static FaitPartieDeExportDto FromModel(FaitPartieDe faitPartieDe)
    {
        return new FaitPartieDeExportDto
        {
            PersonneId = Convert.ToInt32(faitPartieDe.PersonneId),
            EntrepriseId = faitPartieDe.EntrepriseId,
        };
    }
}

public class IngredientExportDto
{
    public int IngredientId { get; set; }
    public string IngredientNom { get; set; }
    public string IngredientVolume { get; set; }
    public string IngredientUnite { get; set; }
}

public static class IngredientExportDtoMapper
{
    public static IngredientExportDto FromModel(Ingredient ingredient)
    {
        return new IngredientExportDto
        {
            IngredientId = ingredient.IngredientId,
            IngredientNom = ingredient.IngredientNom,
            IngredientVolume = ingredient.IngredientVolume,
            IngredientUnite = ingredient.IngredientUnite,
        };
    }
}

public class LivraisonExportDto
{
    public int LivraisonId { get; set; }
    public string LivraisonAdresse { get; set; }
    public DateTime? LivraisonDate { get; set; }
}

public static class LivraisonExportDtoMapper
{
    public static LivraisonExportDto FromModel(Livraison livraison)
    {
        return new LivraisonExportDto
        {
            LivraisonId = livraison.LivraisonId,
            LivraisonAdresse = livraison.LivraisonAdresse,
            LivraisonDate = livraison.LivraisonDate,
        };
    }
}

public class LivreExportDto
{
    public int PlatId { get; set; }
    public int LivraisonId { get; set; }
}

public static class LivreExportDtoMapper
{
    public static LivreExportDto FromModel(Livre livre)
    {
        return new LivreExportDto
        {
            PlatId = livre.PlatId,
            LivraisonId = livre.LivraisonId,
        };
    }
}

public class PersonneExportDto
{
    public string PersonneEmail { get; set; }
    public string PersonneNom { get; set; }
    public string PersonnePrenom { get; set; }
    public string PersonneVille { get; set; }
    public string PersonneCodePostale { get; set; }
    public string PersonneNomDeLaRue { get; set; }
    public string PersonneNumeroDeLaRue { get; set; }
    public string PersonneTelephone { get; set; }
    public string PersonneStationDeMetroLaPlusProche { get; set; }
    public bool? PersonneIsAdmin { get; set; }
}

public static class PersonneExportDtoMapper
{
    public static PersonneExportDto FromModel(Personne personne)
    {
        return new PersonneExportDto
        {
            PersonneEmail = personne.PersonneEmail,
            PersonneNom = personne.PersonneNom,
            PersonnePrenom = personne.PersonnePrenom,
            PersonneVille = personne.PersonneVille,
            PersonneCodePostale = personne.PersonneCodePostale.ToString(),
            PersonneNomDeLaRue = personne.PersonneNomDeLaRue,
            PersonneNumeroDeLaRue = personne.PersonneNumeroDeLaRue.ToString(),
            PersonneTelephone = personne.PersonneTelephone,
            PersonneStationDeMetroLaPlusProche = personne.PersonneStationDeMetroLaPlusProche,
            PersonneIsAdmin = personne.PersonneIsAdmin,
        };
    }
}

public class PlatExportDto
{
    public int PlatId { get; set; }
    public DateTime? PlatDateDeFabrication { get; set; }
    public DateTime? PlatDateDePeremption { get; set; }
    public string PlatPrix { get; set; }
    public string PlatNombrePortion { get; set; }
    public string CuisinierUsername { get; set; }
    public int RecetteId { get; set; }
    public string PlatDuJour { get; set; }
}

public static class PlatExportDtoMapper
{
    public static PlatExportDto FromModel(Plat plat)
    {
        return new PlatExportDto
        {
            PlatId = plat.PlatId,
            PlatDateDeFabrication = plat.PlatDateDeFabrication,
            PlatDateDePeremption = plat.PlatDateDePeremption,
            PlatPrix = plat.PlatPrix,
            PlatNombrePortion = plat.PlatNombrePortion.ToString(),
            CuisinierUsername = plat.CuisinierUsername,
            RecetteId = plat.RecetteId,
            PlatDuJour = plat.PlatDuJour.ToString(),
        };
    }
}

public class RecetteExportDto
{
    public int RecetteId { get; set; }
    public string RecetteNom { get; set; }
    public string RecetteOrigine { get; set; }
    public string RecetteTypeDePlat { get; set; }
    public string RecetteApportNutritifs { get; set; }
    public string RecetteRegimeAlimentaire { get; set; }
}

public static class RecetteExportDtoMapper
{
    public static RecetteExportDto FromModel(Recette recette)
    {
        return new RecetteExportDto
        {
            RecetteId = recette.RecetteId,
            RecetteNom = recette.RecetteNom,
            RecetteOrigine = recette.RecetteOrigine,
            RecetteTypeDePlat = recette.RecetteTypeDePlat,
            RecetteApportNutritifs = recette.RecetteApportNutritifs,
            RecetteRegimeAlimentaire = recette.RecetteRegimeAlimentaire,
        };
    }
}
