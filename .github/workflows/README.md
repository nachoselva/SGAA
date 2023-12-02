# GitHub Actions

Reference: [luizhlelis/elastic-beanstalk-playground](https://github.com/luizhlelis/elastic-beanstalk-playground)

## Pre requisites

> **NOTE:** for more information about configuring open-id connect in AWS, take a look at [this documentation](https://docs.github.com/en/actions/deployment/security-hardening-your-deployments/configuring-openid-connect-in-amazon-web-services).

- Create a policy called `custom-github-actions-policy` on IAM with the following permissions:

```json
{
    "Version": "2012-10-17",
    "Statement": [
        {
            "Sid": "VisualEditor0",
            "Effect": "Allow",
            "Action": "ecr:GetAuthorizationToken",
            "Resource": "*"
        },
        {
            "Sid": "VisualEditor1",
            "Effect": "Allow",
            "Action": [
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
}
```

- Create a role called `custom-github-actions-role` on IAM, this role should be of the type `Web Identity`, select the just created `Identity Provider`. Than select the just created policy and attach it to this role;

- Besides, add both AWS-managed policies to your role: `AWSElasticBeanstalkWebTier` and `AWSElasticBeanstalkManagedUpdatesCustomerRolePolicy`;

- Create a new `Identity Provider` on `IAM > Access Management`, select `OpenID Connect` with the following information:
    - Provider URL: token.actions.githubusercontent.com
    - Audience: sts.amazonaws.com
