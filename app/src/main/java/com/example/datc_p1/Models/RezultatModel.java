package com.example.datc_p1.Models;

import com.google.gson.annotations.SerializedName;

public class RezultatModel {
    @SerializedName("CandidatAles")
    private String CandidatAles;
    @SerializedName("Cnp")
    private String Cnp;
    @SerializedName("Serie")
    private String Serie;
    @SerializedName("Judet")
    private String Judet;

    public RezultatModel() {
    }

    public String getCandidatAles() {
        return CandidatAles;
    }

    public void setCandidatAles(String candidatAles) {
        CandidatAles = candidatAles;
    }

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
