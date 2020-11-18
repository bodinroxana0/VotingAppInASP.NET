package com.example.datc_p1.Activities;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.util.Size;
import android.widget.TextView;

import com.example.datc_p1.Helpers.HttpClientManager;
import com.example.datc_p1.Helpers.StorageHelper;
import com.example.datc_p1.Models.CandidatiModel;
import com.example.datc_p1.Models.RaportVoturiModel;
import com.example.datc_p1.Models.VotantiModel;
import com.example.datc_p1.R;

import org.w3c.dom.Text;

import java.util.ArrayList;
import java.util.List;
import java.util.StringJoiner;

public class StatisticsActivity extends AppCompatActivity {

    private TextView inscrisiTv;
    private TextView voturiTv;
    private TextView prezentaTv;
    private int inscrisi;
    private int votanti;
    private float prezenta;
    private List<RaportVoturiModel> statisticList;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_statistics);

        initializeViews();

        HttpClientManager.getInstance().getListaVotanti(new HttpClientManager.OnDataReceived<List<VotantiModel>>() {
            @Override
            public void dataReceived(List<VotantiModel> data) {
                inscrisi = data.size();

                HttpClientManager.getInstance().getNrVoturi(new HttpClientManager.OnDataReceived<List<RaportVoturiModel>>() {
                    @Override
                    public void dataReceived(List<RaportVoturiModel> data) {
                        setData(data);
                    }

                    @Override
                    public void onFailed() {

                    }
                });

            }

            @Override
            public void onFailed() {

            }
        });

    }

    public void setData(List<RaportVoturiModel> data){
        statisticList = new ArrayList<>();
        statisticList = data;
        for (RaportVoturiModel vot: statisticList
             ) {
            votanti += vot.getNumar();
        }

        prezenta = 100 * (float)votanti / (float)inscrisi;
        String prez = String.format("%.2f", prezenta);

        inscrisiTv.setText(String.valueOf(inscrisi));
        voturiTv.setText(String.valueOf(votanti));
        prezentaTv.setText(prez+"%");
    }

    private void initializeViews(){
        inscrisiTv = findViewById(R.id.tv_inscrisi);
        voturiTv = findViewById(R.id.tv_voturi);
        prezentaTv = findViewById(R.id.tv_prezenta);
    }


}
