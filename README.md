# vTraderEngineAutomationPOC

Welcome to the vTraderEngineAutomationPOC repository! This project contains automated tests for order management functionality using NUnit framework, aimed to validate the creation, modification, and cancellation of orders through a FIX API.

## Table of Contents

- [Installation](#installation)
- [Usage](#usage)
- [Tests](#tests)
- [Contributing](#contributing)
- [License](#license)

## Installation

To run the tests, ensure you have the following prerequisites installed:

- [.NET SDK](https://dotnet.microsoft.com/download) (version compatible with the project)
- [NUnit](https://nunit.org/) for test framework
- [NUnit Console Runner](https://nunit.org/download/) for running tests from the command line (optional)
- [Visual Studio](https://visualstudio.microsoft.com/) or any compatible IDE

Clone the repository:

```bash
git clone https://github.com/yourusername/TestProject.git
cd TestProject
```

Restore the dependencies:

```bash
dotnet restore
```

## Usage

### Running Tests

You can run the tests using the test explorer in your IDE or by using the CLI:

```bash
dotnet test
```

### Test Structure

1. **SetUp**: Initializes the test environment and generates a new order ID before each test.
2. **Tests**:
   - `CreateOrder()`: Tests the creation of a new order.
   - `ModifyOrder()`: Tests modifying an existing order after creating it.
   - `CancelOrder()`: Tests cancelling an order after creating it.
3. **TearDown**: Cleans up any resources after the tests are executed.

## Tests

The tests utilize the `HelperFunctions` class for:
- Sending FIX messages.
- Creating, modifying, and canceling orders.
- Validating responses for each operation.

Each test method logs its progress to the console. The tests are run in order, ensuring that each step depends on the successful execution of the previous step.

**Note**: Ensure the FIX API service is running and accessible before executing the tests.

## Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature/YourFeature`).
3. Make your changes.
4. Commit your changes (`git commit -m 'Add some feature'`).
5. Push to the branch (`git push origin feature/YourFeature`).
6. Open a pull request.

## Author
- **Name:** Muzammil Hussain
- **Position:** Senior Automation Engineer

## Contact
For questions or suggestions, please contact:
- **Email:** muzammil.hussain@logicielservice.com

---