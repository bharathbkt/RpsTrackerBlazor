﻿using Microsoft.AspNetCore.Components;
using RPS.Core.Models;
using RPS.Core.Models.Dto;
using RPS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPS.Web.Server.Components.Backlog
{
    public partial class Chitchat : ComponentBase
    {
        [Parameter]
        public PtItem Item { get; set; }

        public PtUser CurrentUser { get; set; }

        public List<PtComment> CommentItems = new List<PtComment>();


        public string NewCommentTitle = string.Empty;

        [Inject]
        private IPtCommentsRepository RpsCommentsRepo { get; set; }

        [Inject]
        private IPtUserRepository RpsUsersRepo { get; set; }

        protected override void OnInitialized()
        {
            CommentItems = Item.Comments;
            CurrentUser = RpsUsersRepo.GetAll().Where(u => u.Id == 21).FirstOrDefault();
            base.OnInitialized();
        }

        public void AddCommentHandler()
        {
            if (!string.IsNullOrWhiteSpace(NewCommentTitle))
            {
                SaveComment();
                NewCommentTitle = string.Empty;
            }
        }

        private void SaveComment()
        {
            PtNewComment commentNew = new PtNewComment
            {
                ItemId = Item.Id,
                Title = NewCommentTitle
            };

            RpsCommentsRepo.AddNewComment(commentNew);
        }

    }
}
