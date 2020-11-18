package com.example.datc_p1.Models;

import com.google.gson.annotations.SerializedName;

public class UserModel {

    @SerializedName("rowKey")
    private String Cnp;
    @SerializedName("judet")
    private String Judet;
    @SerializedName("partitionKey")
    private String Serie;

    public String getCnp() {
        return Cnp;
    }

    public void setCnp(String cnp) {
        Cnp = cnp;
    }

    public String getJudet() {
        return Judet;
    }

    public void setJudet(String judet) {
        Judet = judet;
    }

    public String getSerie() {
        return Serie;
    }

    public void setSerie(String serie) {
        Serie = serie;
    }
}
