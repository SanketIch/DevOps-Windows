run: |
        $trxPath = Get-ChildItem -Path TestResults -Filter *.trx | Select-Object -First 1
        $xml = [xml](Get-Content $trxPath.FullName)
        $resultsDir = "TestResults"
        $csvPath = "$resultsDir/TestResults.csv"
        $testResults = @()
        $criticalFailed = $false
        $failedCriticalTests = @() # Array to store names of failed critical tests
        foreach ($unitTestResult in $xml.TestRun.Results.UnitTestResult) {
          $testId = $unitTestResult.testId
          $testDefinition = $xml.TestRun.TestDefinitions.UnitTest | Where-Object { $_.id -eq $testId }
          $categories = @()
          if ($testDefinition.TestCategory.TestCategoryItem -is [System.Array]) {
            $categories = @($testDefinition.TestCategory.TestCategoryItem | ForEach-Object { $_.TestCategory })
          } elseif ($testDefinition.TestCategory.TestCategoryItem) {
            $categories = @($testDefinition.TestCategory.TestCategoryItem.TestCategory)
          }
          $isCritical = "Critical" -in $categories
          # Debug Output
          Write-Host "Test Case: $($unitTestResult.testName)"
          Write-Host "IsCritical: $isCritical"
          if ($unitTestResult.outcome -eq "Failed" -and $isCritical) {
            Write-Host "❌ Critical test failed: $($unitTestResult.testName)"
            $criticalFailed = $true
            $failedCriticalTests += $unitTestResult.testName # Add the name to the array
          }
          $testResults += [PSCustomObject]@{
            TestCases  = $unitTestResult.testName
            Outcome    = $unitTestResult.outcome
            StartTime  = $unitTestResult.startTime
            EndTime    = $unitTestResult.endTime
            IsCritical = $isCritical
          }
        }
        $testResults | Export-Csv -Path $csvPath -NoTypeInformation
        if ($criticalFailed) {
          $failedTestNames = $failedCriticalTests -join ", " # Join the names with a comma
          Write-Error "Critical test failure(s) detected: $failedTestNames. Failing workflow."
          exit 1
        } else {
          Write-Host "✅ No critical test failures."
        }
