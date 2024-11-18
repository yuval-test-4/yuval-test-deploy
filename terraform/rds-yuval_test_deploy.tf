module "rds_yuval_test_deploy" {
  source  = "terraform-aws-modules/rds/aws"
  version = "6.2.0"

  identifier = "yuval_test_deploy"

  engine               = "postgres"
  engine_version       = "14"
  family               = "postgres14"
  major_engine_version = "14"
  instance_class       = "db.t4g.medium"

  allocated_storage     = 20
  max_allocated_storage = 100

  db_name  = "yuval_test_deploy"
  username = "postgres"
  password = random_password.yuval_test_deploy_database_password.result
  port     = 5432

  manage_master_user_password = false

  db_subnet_group_name   = module.vpc.database_subnet_group
  vpc_security_group_ids = [module.sg_yuval_test_deploy.security_group_id]

  maintenance_window      = "Mon:00:00-Mon:03:00"
  backup_window           = "03:00-06:00"
  backup_retention_period = 14
}

resource "random_password" "yuval_test_deploy_database_password" {
  length  = 20
  special = false
}

output "db_password_yuval_test_deploy" {
  description = "The password for the master user of the database"
  value       = random_password.yuval_test_deploy_database_password.result
  sensitive   = true
}

output "db_instance_address_yuval_test_deploy" {
  description = "The address of the RDS instance"
  value       = module.rds_yuval_test_deploy.db_instance_address
}

output "db_instance_arn_yuval_test_deploy" {
  description = "The ARN of the RDS instance"
  value       = module.rds_yuval_test_deploy.db_instance_arn
}

output "db_instance_availability_zone_yuval_test_deploy" {
  description = "The availability zone of the RDS instance"
  value       = module.rds_yuval_test_deploy.db_instance_availability_zone
}

output "db_instance_endpoint_yuval_test_deploy" {
  description = "The connection endpoint"
  value       = module.rds_yuval_test_deploy.db_instance_endpoint
}

output "db_instance_engine_yuval_test_deploy" {
  description = "The database engine"
  value       = module.rds_yuval_test_deploy.db_instance_engine
}

output "db_instance_identifier_yuval_test_deploy" {
  description = "The RDS instance identifier"
  value       = module.rds_yuval_test_deploy.db_instance_identifier
}

output "db_instance_name_yuval_test_deploy" {
  description = "The database name"
  value       = module.rds_yuval_test_deploy.db_instance_name
}

output "db_instance_username_yuval_test_deploy" {
  description = "The master username for the database"
  value       = module.rds_yuval_test_deploy.db_instance_username
  sensitive   = true
}

output "db_instance_port_yuval_test_deploy" {
  description = "The database port"
  value       = module.rds_yuval_test_deploy.db_instance_port
}

output "db_subnet_group_id_yuval_test_deploy" {
  description = "The db subnet group name"
  value       = module.rds_yuval_test_deploy.db_subnet_group_id
}

output "db_subnet_group_arn_yuval_test_deploy" {
  description = "The ARN of the db subnet group"
  value       = module.rds_yuval_test_deploy.db_subnet_group_arn
}

output "db_parameter_group_id_yuval_test_deploy" {
  description = "The db parameter group id"
  value       = module.rds_yuval_test_deploy.db_parameter_group_id
}

output "db_parameter_group_arn_yuval_test_deploy" {
  description = "The ARN of the db parameter group"
  value       = module.rds_yuval_test_deploy.db_parameter_group_arn
}
