﻿using BlogProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogProject.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class CommentMap : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.AuthorName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(500).IsRequired();
            builder.Property(x => x.AuthorEmail).HasMaxLength(100).IsRequired();

            builder.HasOne(x => x.ParentComment).WithMany(x => x.SubComments).HasForeignKey(x => x.ParentCommentId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
