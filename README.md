# Capture API

A small, high-precision endpoint assessment and verification
tool for QA-2.0 practitioners exercising APIs.

Structures and grammar exist for verifying adherence to protocol,
headers logic, conformance to contract, relay integrity.

```c#
using (var interaction = Capture(request)) {
    interaction.Response.StatusCode.Should().Be(StatusCode.OK);
}
```

Extensions for
* `HTTPClient.Capture` 
* `(PrintableHttpRequest)request`
* `(PrintableHttpResponse)response`

* ### Environment Variable Binding

```c#
[EnvironmentVariable]
public string Host { get; set; }
```

## What's in this repository?

The CaptureApi library, a unit test suite and a toy API for illustration.

This package depends on [Mountebank](https://www.mbtest.org) to set up
external APIs for mocking communications made by the API under test.

Mountebank should be running on the default port.

## Some extras and examples for illustration.

The [imposters](./Tqxr.Capture.Tests/imposters) folder contains [equals.http](./Tqxr.Capture.Tests/imposters/equals.http)
which can be used to verify that mountebank is running on the local machine.

To run the `.http` file:

```bash
echo equals.http | nc localhost 2525
```

This will tell Mountebank to set up an imposter.

For more information about what your Mountebank can do, make sure it is running, and:

```bash
npx mountebank
http localhost:2525/imposters
```

And browse Mountebank's [built-in documentation](http://localhost:2525).



---
© **2019** _What Bird Is That?_
