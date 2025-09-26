# 🧠 Lost Children - AI-Powered Child Recovery Platform

## Problem Statement
Every year, thousands of children go missing, creating heartbreaking challenges for families. Traditional search methods struggle with two critical factors:

- **Scale**: Searching through hundreds of found children records manually
- **Time**: Children's appearances change significantly as they grow older, making visual identification nearly impossible over time

## 🚀 Our Solution
**Lost Children** is a comprehensive web platform that leverages artificial intelligence to revolutionize how we search for missing children. By combining facial recognition with face aging technology, we bridge the gap between lost children and their families.

## ✨ Key Features
- **AI-Powered Search**: Upload a childhood photo + age information → Our GAN model generates aged progression → Facial recognition matches against found children database
- **Dual Database System**: 
  - **Lost Children**: Reported by searching parents/family
  - **Found Children**: Reported by shelters, organizations, or individuals
- **Smart Matching**: Returns Top 10 most similar matches with similarity scoring
- **Automated Reporting**: Streamlined process for reporting both lost and found children
- **Secure Platform**: Role-based access control and secure data handling

## 🛠️ Technical Implementation
- **Backend**: ASP.NET Core MVC with RESTful APIs
- **AI Integration**: Custom GAN model for face aging + facial recognition API
- **Database**: SQL Server with optimized schema for image metadata
- **Cloud Storage**: Cloudinary for efficient image processing and storage
- **Frontend**: Responsive design with AJAX for enhanced user experience

## 🔗 Live Demo
🌐 **Web Application**: [https://lostchildren.runasp.net/](https://lostchildren.runasp.net/)

## 📊 How It Works
1. **Report Missing Child**: Parents upload recent photos and details
2. **AI Processing**: GAN model generates age-progressed images
3. **Database Matching**: Facial recognition scans found children database
4. **Results Delivery**: Top 10 matches delivered with similarity scores
5. **Continuous Monitoring**: Unmatched reports remain active for future comparisons

## 🌟 Impact
This platform has the potential to significantly reduce the time and resources needed to reunite families, bringing hope to what is often a desperate situation. By harnessing the power of AI, we're creating a more efficient, scalable solution to a critical social problem.
