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


---
© **2019** _What Bird Is That?_
