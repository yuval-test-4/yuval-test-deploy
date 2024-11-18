resource "random_password" "yuval_test_deploy_secret_password" {
  length  = 20
  special = false
}

resource "aws_secretsmanager_secret" "secrets_yuval_test_deploy" {
  name = "yuval_test_deploy_secrets"
}

resource "aws_secretsmanager_secret_version" "secrets_version_yuval_test_deploy" {
  secret_id     = aws_secretsmanager_secret.secrets_yuval_test_deploy.id
  secret_string = jsonencode({
    BCRYPT_SALT       = "10"
    JWT_EXPIRATION    = "2d"
    JWT_SECRET_KEY    = random_password.yuval_test_deploy_secret_password.result
    DB_URL     = "Server=${module.rds_yuval_test_deploy.db_instance_address};Port=5432;Database=${module.rds_yuval_test_deploy.db_instance_name};User Id=${module.rds_yuval_test_deploy.db_instance_username};Password=${random_password.yuval_test_deploy_database_password.result};"
  })
}
