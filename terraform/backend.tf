terraform {
  backend "s3" {
    bucket = "terraform-state-demonstration"
    key    = "development/yuvaltest"
    region = "us-east-1"
  }
}