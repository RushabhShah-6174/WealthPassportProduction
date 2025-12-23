# DevOps Engineer - Technical Assignment

Welcome, and thank you for your interest in the DevOps Engineer role at CaseGuard.

This assignment is designed to evaluate your **infrastructure design** and **execution skills**. Your task is to deploy the ProductManagementSystem API to AWS using Infrastructure as Code (AWS CDK in C#). This is meant to be a **couple hours of focused work** - we want to see how you approach problems and make architectural decisions, not perfection.

**Focus on**: Making it work properly, then documenting your choices and reasoning so we can discuss them in the next interview.

---

## Repository Structure

This repository contains two projects:

1. **ProductManagementSystem**
   An ASP.NET Core Web API that exposes basic CRUD operations on a `Products` table using DynamoDB.

2. **InfrastructureAsCode**
   A .NET console project where you will write AWS CDK code in C#. This project is currently empty aside from `Program.cs`.

---

## Prerequisites

Please make sure you have the following installed:
- .NET 8 SDK
- Docker
- AWS CLI (configured with a free tier AWS account)
- AWS CDK v2 (with support for C#)

---

## Getting Started Locally

1. **Run DynamoDB locally**
   Start a DynamoDB container on the default port (8000):
   ```bash
   docker run -d -p 8000:8000 amazon/dynamodb-local
   ```

2. **Run the API**
   Navigate to the `ProductManagementSystem` directory and start the API:
   ```bash
   dotnet run
   ```

3. **Test the API**
   - Swagger UI is enabled in the `Development` environment at `http://localhost:5000`
   - For `Staging` and `Production`, use Postman or another API client

---

## Your Assignment

### What to Build

Deploy the ProductManagementSystem API to AWS with:
- **Single AWS region** deployment
- **Two environments**: staging and production
- **Containerized** API using your choice of compute service
- **Infrastructure as Code** using AWS CDK in C#
- Working CRUD operations against DynamoDB in both environments

### Requirements

- Use AWS CDK in C# to define all infrastructure
- Choose appropriate AWS services
- Separate staging and production configurations
- Secure setup (no hardcoded secrets, appropriate IAM roles, private/public subnets as needed)
- Both environments should be deployable with CDK commands

### What We're Evaluating

1. **Functionality**: Does it work? Can we successfully deploy it following your documentation?
2. **AWS Service Selection**: Did you choose appropriate services for this use case?
3. **Code Quality**: Is your CDK code organized, readable, and maintainable?
4. **Configuration Management**: How well do you handle environment-specific settings?
5. **Documentation**: Can we understand your architectural choices and follow along your reasoning?

### What to Document

Keep it concise and focus on these areas:

1. **How to Deploy**
   Step-by-step instructions to deploy both staging and production environments. Include prerequisites, setup steps, and CDK commands.

2. **Architecture Decisions**
   - Which AWS services you chose and why
   - How you structured the CDK project
   - How you handle differences between staging and production
   - Any trade-offs or assumptions you made

3. **What's Next**
   If you had more time and resources, what would you add or improve? Examples:
   - Multi-region deployment approach
   - CI/CD pipeline design
   - Monitoring and observability
   - Security enhancements
   - Cost optimization strategies
   - Auto-scaling configuration

---

## Submission

1. **Push your code** to this repository
2. **Update this README** with:
   - Your deployment instructions
   - Your architecture decisions and reasoning
   - Known limitations or issues (if any)
   - What you would do next with more time

---

## Important Notes

- You have **complete freedom** to choose the specific AWS services that best fit the requirements
- **Focus on making it work properly first**, then document your reasoning
- We value your **thought process and approach** along with complete implementation
- If you don't finish all aspects, clearly document:
  - What you accomplished
  - Any challenges you faced
  - How you would proceed with more time
- You **don't need to keep your infrastructure deployed** after submission - we'll evaluate by deploying it ourselves
- Be specific about any technical decisions you made and why

---

Good luck, and we look forward to your solution!
