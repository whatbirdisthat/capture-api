# Capture API

A small, high-precision endpoint assessment and verification
tool for QA-2.0 practitioners.

Structures and grammar exist for varifying adherence to protocol,
headers logic, conformance to contract.

```
using (var interaction = Capture(request)) {
    interaction.Response.StatusCode.Should().Be(StatusCode.OK);
}
```

Extensions for HTTPClient, Request, Response.

(C) 2019 What Bird Is That?
