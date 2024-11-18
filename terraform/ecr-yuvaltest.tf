module "ecr_yuvaltest" {
  source  = "terraform-aws-modules/ecr/aws"
  version = "1.6.0"

  repository_name                   = "yuvaltest"
  repository_type                   = "private"
  create_lifecycle_policy           = false
  repository_force_delete           = false
  repository_read_access_arns       = []
  repository_read_write_access_arns = []
}

output "repository_arn_yuvaltest" {
  description = "Full ARN of the repository"
  value       = module.ecr_yuvaltest.repository_arn
}

output "repository_registry_id_yuvaltest" {
  description = "The registry ID where the repository was created"
  value       = module.ecr_yuvaltest.repository_registry_id
}

output "repository_url_yuvaltest" {
  description = "The URL of the repository (in the form `aws_account_id.dkr.ecr.region.amazonaws.com/repositoryName`)"
  value       = module.ecr_yuvaltest.repository_url
}
