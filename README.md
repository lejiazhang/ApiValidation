# ApiValidation
.net core api client validation generator

# {url}/validation

# use for client validations generator.

# use fluentValidation

Sapmle validation for client:

{
    "financialProductQuery": {
        "fields": {
            "assetModelId": {
                "rules": {
                    "required": {
                        "required": true
                    },
                    "greaterThan": {
                        "value": 0,
                        "message": "Please specify AssetModelId."
                    },
                    "regex": {
                        "regex": "^([-]?[1-9]\\d*|0)$",
                        "ignoreCase": false,
                        "message": "Field value must be integer"
                    }
                }
            },
            "companyId": {
                "rules": {
                    "required": {
                        "required": true
                    },
                    "greaterThan": {
                        "value": 0,
                        "message": "Please specify CompanyId."
                    },
                    "regex": {
                        "regex": "^([-]?[1-9]\\d*|0)$",
                        "ignoreCase": false,
                        "message": "Field value must be integer"
                    }
                }
            },
            "modelYear": {
                "rules": {
                    "required": {
                        "required": true
                    },
                    "greaterThan": {
                        "value": 0,
                        "message": "Please specify ModelYear."
                    },
                    "regex": {
                        "regex": "^([-]?[1-9]\\d*|0)$",
                        "ignoreCase": false,
                        "message": "Field value must be integer"
                    }
                }
            },
            "nst": {
                "rules": {
                    "required": {
                        "required": true,
                        "message": "Please specify NST."
                    }
                }
            },
            "pmsDealerNbr": {
                "rules": {
                    "required": {
                        "required": true,
                        "message": "Please specify PMSDealerNbr."
                    }
                }
            }
        }
    }
}
