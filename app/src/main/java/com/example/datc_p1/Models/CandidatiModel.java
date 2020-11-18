package com.example.datc_p1.Models;

import com.google.gson.annotations.SerializedName;

import java.util.Date;

public class CandidatiModel {

    @SerializedName("partidSigla")
    private String PartidSigla;
    @SerializedName("rowKey")
    private String NumePrenume;
    @SerializedName("partitionKey")
    private String Partid;
    @SerializedName("timestamp")
    private Date Timestamp;
    private boolean isChecked;

    public CandidatiModel() {

    }

    public Date getTimestamp() {
        return Timestamp;
    }

    public void setTimestamp(Date timestamp) {
        Timestamp = timestamp;
    }

    public boolean isChecked() {
        return isChecked;
    }

    public void setChecked(boolean checked) {
        isChecked = checked;
    }

    public String getPartid() {
        return Partid;
    }

    public void setPartid(String partid) {
        Partid = partid;
    }

    public String getNumePrenume() {
        return NumePrenume;
    }

    public void setNumePrenume(String numePrenume) {
        NumePrenume = numePrenume;
    }

    public String getPartidSigla() {
        return PartidSigla;
    }

    public void setPartidSigla(String partidSigla) {
        PartidSigla = partidSigla;
    }
}
