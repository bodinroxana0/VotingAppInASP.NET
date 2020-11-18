package com.example.datc_p1.Activities;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.os.Message;
import android.util.Size;
import android.view.View;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.example.datc_p1.Helpers.HttpClientManager;
import com.example.datc_p1.Helpers.StorageHelper;
import com.example.datc_p1.Models.CandidatiModel;
import com.example.datc_p1.Models.UserModel;
import com.example.datc_p1.Models.VotantiModel;
import com.example.datc_p1.R;

import java.util.ArrayList;
import java.util.List;

public class LoginActivity extends AppCompatActivity {

    private EditText cnpEt;
    private EditText serieEt;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);
        initializeViews();
        cnpEt.setText("2910616115807");
        serieEt.setText("ZH014531");
    }

    public void OnSignIn(View view){

        if(cnpEt.getText().toString().isEmpty() || serieEt.getText().toString().isEmpty()){
            Toast.makeText(this, "Introduceți valori în toate câmpurile", Toast.LENGTH_LONG).show();
            return;
        }

        if(cnpEt.getText().length()!=13)
        {
            Toast.makeText(this, "CNP-ul trebuie să aibă 13 caractere", Toast.LENGTH_LONG).show();
            return;
        }

        final String cnp= cnpEt.getText().toString();
        final String serie= serieEt.getText().toString();

        HttpClientManager.getInstance().Login(cnp, serie, new HttpClientManager.OnDataReceived
                <List<UserModel>>() {
            @Override
            public void dataReceived(List<UserModel> data) {
                if (data.size() == 0)
                {
                    Toast.makeText(LoginActivity.this, "Persoana cu seria de buletin " + serie + " nu poate vota.", Toast.LENGTH_SHORT).show();
                    return;
                }
                StorageHelper.myUser = data.get(0);
                Intent myInt= new Intent(LoginActivity.this, DetailActivity.class);
                startActivity(myInt);
            }

            @Override
            public void onFailed() {

            }
        });
    }

    private void initializeViews(){
        cnpEt= findViewById(R.id.et_cnp);
        serieEt= findViewById(R.id.et_serie);
    }

//    private void getCandidates()
//    {
//        StorageHelper.myCandidatiList = new ArrayList<>();
//
//        HttpClientManager.getInstance().getCandidates(new HttpClientManager.OnDataReceived<List<CandidatiModel>>() {
//            @Override
//            public void dataReceived(List<CandidatiModel> data) {
//                StorageHelper.myCandidatiList.addAll(data);
//            }
//
//            @Override
//            public void onFailed() {
//
//            }
//        });
//    }
}
