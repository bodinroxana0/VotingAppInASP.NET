package com.example.datc_p1.Adapters;


import android.content.Context;
import android.graphics.Color;
import android.graphics.drawable.Drawable;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import com.bumptech.glide.Glide;
import com.bumptech.glide.load.DataSource;
import com.bumptech.glide.load.engine.GlideException;
import com.bumptech.glide.request.RequestListener;
import com.bumptech.glide.request.target.Target;
import com.example.datc_p1.Models.CandidatiModel;
import com.example.datc_p1.R;

import java.util.List;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.cardview.widget.CardView;
import androidx.recyclerview.widget.RecyclerView;

public class CandidatesAdapter extends RecyclerView.Adapter<CandidatesAdapter.ViewHolder> {
    private List<CandidatiModel> candidatesPosts;
    private Context context;
    private String chosenCandidate;
    private int currentPicture = 0;

    public String getChosenCandidate() {
        return chosenCandidate;
    }

    public CandidatesAdapter(List<CandidatiModel> candidatesPosts, Context context) {
        this.candidatesPosts = candidatesPosts;
        this.context = context;
    }

    @NonNull
    @Override
    public CandidatesAdapter.ViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        Context context = parent.getContext();
        LayoutInflater layoutInflater = LayoutInflater.from(context);
        View view = layoutInflater.inflate(R.layout.row_candidates, parent, false);
        ViewHolder holder = new ViewHolder(view);
        return holder;
    }

    @Override
    public void onBindViewHolder(@NonNull final ViewHolder holder, final int position) {
        String name = candidatesPosts.get(position).getNumePrenume();
        String image = candidatesPosts.get(position).getPartidSigla();
        boolean isChecked = candidatesPosts.get(position).isChecked();

        holder.nameTv.setText(name);
        Glide.with(holder.nameIv)
                .load(image)
                .centerCrop()
                .listener(new RequestListener<Drawable>() {
                    @Override
                    public boolean onLoadFailed(@Nullable GlideException e, Object model, Target<Drawable> target, boolean isFirstResource) {
                        return false;
                    }

                    @Override
                    public boolean onResourceReady(Drawable resource, Object model, Target<Drawable> target, DataSource dataSource, boolean isFirstResource) {
                        currentPicture++;
                        if (currentPicture == getItemCount())
                        {
                            int a = 5;
                        }
                        return false;
                    }
                })
                .into(holder.nameIv);

        if (isChecked)
        {
            holder.candidatesCv.setCardBackgroundColor(Color.parseColor("#FF9700"));
        }
        else
        {
            holder.candidatesCv.setCardBackgroundColor(Color.parseColor("#FFFFFF"));
        }

        holder.itemView.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                for (CandidatiModel canditates: candidatesPosts) {
                    canditates.setChecked(false);
                }
                candidatesPosts.get(position).setChecked(true);
                chosenCandidate = candidatesPosts.get(position).getNumePrenume();
               notifyDataSetChanged();
            }
        });
    }

    @Override
    public int getItemCount() {
        return candidatesPosts.size();
    }

    public class ViewHolder extends RecyclerView.ViewHolder {

        private TextView nameTv;
        private ImageView nameIv;
        private CardView candidatesCv;

        public ViewHolder(@NonNull View itemView) {
            super(itemView);
            initializeViews(itemView);
        }

        private void initializeViews(View itemView) {
            nameTv = itemView.findViewById(R.id.tv_candidate);
            nameIv = itemView.findViewById(R.id.iv_candidate);
            candidatesCv = itemView.findViewById(R.id.cv_candidates);
        }
    }
}