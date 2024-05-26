﻿using Leren1.Masters;
using Leren1.Models;
using Leren1.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Leren1.Pages
{
    public partial class ArticleCreation : System.Web.UI.Page
    {
        private static int[] Sections = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        private static List<SubjectHeader> subjectHeaders;
        private static List<CategoryHeader> categoryHeaders;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                DatabaseEntities1 db = DatabaseSingleton.GetInstance();
                subjectHeaders = (from s in db.SubjectHeaders select s).ToList();
                categoryHeaders = (from c in db.CategoryHeaders select c).ToList();
                DropDownCategory.DataSource = categoryHeaders.Select(c => c.CategoryTitle).ToList();
                DropDownSubjects.DataSource = subjectHeaders.Select(s => s.SubjectTitle).ToList();
                DropDownListSections.DataSource = Sections;
                DropDownCategory.DataBind();
                DropDownSubjects.DataBind();
                DropDownListSections.DataBind();
            }
        }

        private String GenerateArticleID()
        {
            Random rnd = new Random();
            int X = rnd.Next(1, 1000);
            String ArticleID = String.Format("AR{0:000}", X);
            return ArticleID;
        }

        protected void ButtonCreate_Click(object sender, EventArgs e)
        {
            String articleId = GenerateArticleID();
            String title = TextTitle.Text;
            String subject = DropDownSubjects.Text;
            String category = DropDownCategory.Text;
            String sections = DropDownListSections.Text;
            String subjectId = subjectHeaders.Where(s => s.SubjectTitle == subject).Select(s => s.SubjectID).FirstOrDefault().ToString();
            String categoryId = categoryHeaders.Where(c => c.CategoryTitle == category).Select(c => c.CategoryID).FirstOrDefault().ToString();

            ArticleHeader newArticle = new ArticleHeader()
            {
                ArticleTitle = title,
                ArticleID = articleId,
                SubjectID = subjectId,
                CategoryID = categoryId,
                Sections = Convert.ToInt32(sections),
                UserID = ((LoggedIn)this.Master).curUser.Id
            };

            DatabaseEntities1 db = DatabaseSingleton.GetInstance();
            db.ArticleHeaders.Add(newArticle);
            db.SaveChanges();

            Response.Redirect("~/Pages/SectionsCreation.aspx?Title=" + title + "&ID=" + articleId + "&Subject=" + subject + "&Category=" + category + "&Sections=" + sections + "&IsUpdate=0");
        }
    }
}