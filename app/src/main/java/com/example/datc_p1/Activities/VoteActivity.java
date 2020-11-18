package com.example.datc_p1.Activities;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Toast;

import com.example.datc_p1.Adapters.CandidatesAdapter;
import com.example.datc_p1.Helpers.HttpClientManager;
import com.example.datc_p1.Helpers.StorageHelper;
import com.example.datc_p1.Models.CandidatiModel;
import com.example.datc_p1.Models.RezultatModel;
import com.example.datc_p1.Models.UserModel;
import com.example.datc_p1.R;

import java.util.ArrayList;
import java.util.List;

import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

public class VoteActivity extends AppCompatActivity {


    private RecyclerView recyclerView;
    private CandidatesAdapter candidatesAdapter;
    private List<CandidatiModel> candidatesList;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_vote);
        initializeViews();

        HttpClientManager.getInstance().getCandidati(new HttpClientManager.OnDataReceived<List<CandidatiModel>>() {
            @Override
            public void dataReceived(List<CandidatiModel> data) {
                setRecyclerView(data);
            }

            @Override
            public void onFailed() {

            }
        });

    }

    public void setRecyclerView(List<CandidatiModel> data){
        candidatesList = new ArrayList<>();
        candidatesList = data;
        recyclerView.setLayoutManager(new LinearLayoutManager(getApplicationContext()));
        candidatesAdapter = new CandidatesAdapter(candidatesList,getApplicationContext());
        recyclerView.setAdapter(candidatesAdapter);
    }

    public void GoVote(View view){
        String selectedCandidate = candidatesAdapter.getChosenCandidate();
        RezultatModel rezultatModel = new RezultatModel();
        rezultatModel.setCandidatAles(selectedCandidate);
        rezultatModel.setCnp(StorageHelper.myUser.getCnp());
        rezultatModel.setJudet(StorageHelper.myUser.getJudet());
        rezultatModel.setSerie(StorageHelper.myUser.getSerie());

        if (selectedCandidate == null)
        {
            Toast.makeText(this, "Trebuie să selectezi un candidat", Toast.LENGTH_LONG).show();
            return;
        }
        HttpClientManager.getInstance().postVote(this, rezultatModel, new HttpClientManager.OnDataReceived<Object>() {
            @Override
            public void dataReceived(Object data) {
                Intent myInt = new Intent(VoteActivity.this, FinishActivity.class);
                startActivity(myInt);
            }

            @Override
            public void onFailed() {
            }
        });

    }

    private void initializeViews(){
        recyclerView = findViewById(R.id.rv_listed_candidates);}
}
