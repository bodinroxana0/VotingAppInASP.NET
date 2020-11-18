package com.example.datc_p1.Helpers;

import com.example.datc_p1.Models.CandidatiModel;
import com.example.datc_p1.Models.RaportVoturiModel;
import com.example.datc_p1.Models.RezultatModel;
import com.example.datc_p1.Models.UserModel;
import com.example.datc_p1.Models.VotantiModel;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.Path;

public interface WebApiService {

    @GET("vot/{partitionKey}")
    Call<List<UserModel>> LogIn(@Path(value = "partitionKey", encoded = true) String partitionKey );

    @GET("vot")
    Call<List<VotantiModel>> getListaVotanti();

    @GET("candidat")
    Call<List<CandidatiModel>> getCandidati();

    @POST("rezultate")
    Call<Object> postVote(@Body RezultatModel rezultatModel);

    @GET("raportvoturi")
    Call<List<RaportVoturiModel>> getNrVoturi();

}
