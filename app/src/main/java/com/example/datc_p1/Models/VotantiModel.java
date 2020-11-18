package com.example.datc_p1.Models;

import com.google.gson.annotations.SerializedName;

import java.util.Date;

public class VotantiModel {

    @SerializedName("nume")
    private String Nume;
    @SerializedName("prenume")
    private String Prenume;
    @SerializedName("rowKey")
    private String RowKey;
    @SerializedName("partitionKey")
    private String PartitionKey;
    @SerializedName("judet")
    private String Judet;
    @SerializedName("localitate")
    private String Localitate;
    @SerializedName("votat")
    private Boolean Votat;
    @SerializedName("timestamp")
    private Date Timestamp;

    public VotantiModel() {
    }

    public String getNume() {
        return Nume;
    }

    public void setNume(String nume) {
        Nume = nume;
    }

    public String getPrenume() {
        return Prenume;
    }

    public void setPrenume(String prenume) {
        Prenume = prenume;
    }

    public String getRowKey() {
        return RowKey;
    }

    public void setRowKey(String rowKey) {
        RowKey = rowKey;
    }

    public String getPartitionKey() {
        return PartitionKey;
    }

    public void setPartitionKey(String partitionKey) {
        PartitionKey = partitionKey;
    }

    public String getJudet() {
        return Judet;
    }

    public void setJudet(String judet) {
        Judet = judet;
    }

    public String getLocalitate() {
        return Localitate;
    }

    public void setLocalitate(String localitate) {
        Localitate = localitate;
    }

    public Boolean getVotat() {
        return Votat;
    }

    public void setVotat(Boolean votat) {
        Votat = votat;
    }

    public Date getTimestap() {
        return Timestamp;
    }

    public void setTimestap(Date timestap) {
        Timestamp = timestap;
    }
}
