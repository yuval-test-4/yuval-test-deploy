terraform {
  backend "s3" {
    bucket = "terraform-state-demonstration"
    key    = "development/yuval_test_deploy"
    region = "us-east-1"
  }
}