package com.example.datc_p1.Models;

import com.google.gson.annotations.SerializedName;

public class RaportVoturiModel {

    @SerializedName("partitionKey")
    private String numeCandidat;
    @SerializedName("rowKey")
    private int numar;

    public String getNumeCandidat() {
        return numeCandidat;
    }

    public void setNumeCandidat(String numeCandidat) {
        this.numeCandidat = numeCandidat;
    }

    public int getNumar() {
        return numar;
    }

    public void setNumar(int numar) {
        this.numar = numar;
    }
}
