# pre requisites

resource "aws_iam_policy" "custom_github_actions_policy" {
  name        = "custom-github-actions-policy"
  path        = "/"
  description = "[SGAA] Policy for GitHub Actions to access AWS resources"

  # Terraform "jsonencode" function converts a Terraform expression result to valid JSON syntax.
  policy = jsonencode({
    "Version" : "2012-10-17",
    "Statement" : [
      {
        "Sid" : "VisualEditor0",
        "Effect" : "Allow",
        "Action" : "ecr:GetAuthorizationToken",
        "Resource" : "*"
      },
      {
        "Sid" : "VisualEditor1",
        "Effect" : "Allow",
        "Action" : [
          "ecr:GetDownloadUrlForLayer",
          "ecr:BatchGetImage",
          "ecr:CompleteLayerUpload",
          "ecr:UploadLayerPart",
          "ecr:InitiateLayerUpload",
          "ecr:BatchCheckLayerAvailability",
          "ecr:PutImage"
        ],
        "Resource" : "arn:aws:ecr:us-east-2:417268992578:repository/sgaa"
      }
    ]
  })
}

resource "aws_ecr_repository" "sgaa_api_production" {
  name                 = "sgaa"
  image_tag_mutability = "MUTABLE"

  image_scanning_configuration {
    scan_on_push = true
  }
}

# elastic beanstalk infrastructure

data "aws_elastic_beanstalk_solution_stack" "multi_docker" {
  most_recent = true
  name_regex  = "^64bit Amazon Linux 2 (.*) running Docker(.*)$"
}

resource "aws_elastic_beanstalk_application" "sgaa_api" {
  name        = "sgaa_api"
  description = "SGAA API"
}

resource "aws_elastic_beanstalk_environment" "sgaa_api_production" {
  name                = "production"
  application         = aws_elastic_beanstalk_application.sgaa_api.name
  solution_stack_name = data.aws_elastic_beanstalk_solution_stack.multi_docker.id
  setting {
    namespace = "aws:autoscaling:launchconfiguration"
    name      = "IamInstanceProfile"
    value     = "aws-elasticbeanstalk-ec2-role"
  }
}