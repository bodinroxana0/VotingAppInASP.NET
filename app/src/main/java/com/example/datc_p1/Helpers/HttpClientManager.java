package com.example.datc_p1.Helpers;

import android.content.Context;
import android.widget.Toast;

import com.example.datc_p1.Activities.VoteActivity;
import com.example.datc_p1.Models.CandidatiModel;
import com.example.datc_p1.Models.RaportVoturiModel;
import com.example.datc_p1.Models.RezultatModel;
import com.example.datc_p1.Models.UserModel;
import com.example.datc_p1.Models.VotantiModel;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import java.io.IOException;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import okhttp3.Interceptor;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.Response;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class HttpClientManager {

    public interface OnDataReceived<T> {
        /**
         * This method is called whenever an api call has been successful.
         *
         * @param data The data from the api call with a generic type.
         */
        void dataReceived(T data);

        /**
         * This method is called whenever an api call has failed.
         */
        void onFailed();
    }
    private static final HttpClientManager instance = new HttpClientManager();
    public static HttpClientManager getInstance() {
        return instance;
    }

    private WebApiService service;

    private HttpClientManager() {
        OkHttpClient.Builder httpClient = new OkHttpClient.Builder();
        httpClient.addInterceptor(new Interceptor() {
            @Override
            public Response intercept(Chain chain) throws IOException {
                Request original = chain.request();

                Request.Builder request = original.newBuilder()
                        .method(original.method(), original.body());

                return chain.proceed(request.build());
            }
        });

        Gson gson = new GsonBuilder().
                setDateFormat("yyyy-MM-dd'T'HH:mm:ss")
                .create();
        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl("https://alegeri.azurewebsites.net/api/")


                .addConverterFactory(GsonConverterFactory.create(gson))
                .client(httpClient.build())
                .build();
        service = retrofit.create(WebApiService.class);
    }

    public void Login(String cnp, String serie, final OnDataReceived<List<UserModel>> callback)
        {
            Map<String, String> map = new HashMap<>();
            map.put("rowKey", cnp);
            map.put("partitionKey",serie);

        Call<List<UserModel>> tokens = service.LogIn(serie);
        tokens.enqueue(new Callback<List<UserModel>>() {
            @Override
            public void onResponse(Call<List<UserModel>> call, retrofit2.Response<List<UserModel>> response) {
                if(response.isSuccessful()) {
                    List<UserModel> a = response.body();
                    callback.dataReceived(a);
                }
            }

            @Override
            public void onFailure(Call<List<UserModel>> call, Throwable t) {
            }
        });
    }

    public void getListaVotanti(final OnDataReceived<List<VotantiModel>> callback){
        Call<List<VotantiModel>> lista = service.getListaVotanti();
        lista.enqueue(new Callback<List<VotantiModel>>() {
            @Override
            public void onResponse(Call<List<VotantiModel>> call, retrofit2.Response<List<VotantiModel>> response) {
                if(response.isSuccessful()) {
                    List<VotantiModel> a = response.body();
                    callback.dataReceived(a);
                }
            }

            @Override
            public void onFailure(Call<List<VotantiModel>> call, Throwable t) {

            }
        });
    }

    public void postVote(final Context context, RezultatModel rezultatModel, final OnDataReceived<Object> callback){
        Call<Object> listaVotat = service.postVote(rezultatModel);
        listaVotat.enqueue(new Callback<Object>() {
            @Override
            public void onResponse(Call<Object> call, retrofit2.Response<Object> response) {
                if(response.isSuccessful()) {
                    Object a = response.body();
                    callback.dataReceived(a);
                }
                else
                {
                    Toast.makeText(context, "Ai votat deja o dată", Toast.LENGTH_LONG).show();
                    return;
                }
            }

            @Override
            public void onFailure(Call<Object> call, Throwable t) {

            }
        });
    }

    public void getCandidati(final OnDataReceived<List<CandidatiModel>> callback){
        Call<List<CandidatiModel>> listaCandidati = service.getCandidati();
        listaCandidati.enqueue(new Callback<List<CandidatiModel>>() {
            @Override
            public void onResponse(Call<List<CandidatiModel>> call, retrofit2.Response<List<CandidatiModel>> response) {
                if(response.isSuccessful()){
                    callback.dataReceived(response.body());
                }
            }

            @Override
            public void onFailure(Call<List<CandidatiModel>> call, Throwable t) {

            }
        });

    }

    public void getNrVoturi(final HttpClientManager.OnDataReceived<List<RaportVoturiModel>> callback){
        Call<List<RaportVoturiModel>> listaVoturi = service.getNrVoturi();
        listaVoturi.enqueue(new Callback<List<RaportVoturiModel>>() {
            @Override
            public void onResponse(Call<List<RaportVoturiModel>> call, retrofit2.Response<List<RaportVoturiModel>> response) {
                if(response.isSuccessful()){
                    callback.dataReceived(response.body());
                }
            }

            @Override
            public void onFailure(Call<List<RaportVoturiModel>> call, Throwable t) {

            }
        });
    }
}
